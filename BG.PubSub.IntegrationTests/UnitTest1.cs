// namespace BG.PubSub.IntegrationTests;

using AutoFixture;
using AutoFixture.AutoMoq;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Ductus.FluentDocker.Model.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

public class UnitTest1 : IClassFixture<MyTestFixture>
{
    [Fact]
    public async void Test1()
    {
        Trace.WriteLine($"method: {nameof(Test1)}");
        //var fixture = new Fixture().Customize(new AutoMoqCustomization());
        //var fluent = new MyTestFixture();
        //Arrange
        // var evento = fixture.Create<CriaAlunoEvent>();
        // await RabbitFixture.Produce(evento);
        // //Act

        // // Assert
        // var criaAlunoEvent = RabbitFixture
        //     .Consume<CriaAlunoEvent>();
    }
}

public abstract class DockerComposeTestBase : IDisposable
{
    protected ICompositeService CompositeService;
    protected IHostService? DockerHost;

    public DockerComposeTestBase()
    {
        EnsureDockerHost();

        Trace.WriteLine($"method: {nameof(Build)}");
        CompositeService = Build();
        try
        {
            Trace.WriteLine($"method: Start");
            CompositeService.Start();
        }
        catch
        {
            Trace.WriteLine($"method: {nameof(Dispose)}");
            CompositeService.Dispose();
            throw;
        }

        OnContainerInitialized();
    }

    public void Dispose()
    {
        OnContainerTearDown();
        var compositeService = CompositeService;
        CompositeService = null!;
        try
        {
            compositeService?.Dispose();
        }
        catch
        {
            // ignored
        }
    }

    protected abstract ICompositeService Build();

    protected virtual void OnContainerTearDown()
    {
        Trace.WriteLine($"method: {nameof(OnContainerTearDown)}");
    }

    protected virtual void OnContainerInitialized()
    {
        Trace.WriteLine($"method: {nameof(OnContainerInitialized)}");

    }

    private void EnsureDockerHost()
    {
        if (DockerHost?.State == ServiceRunningState.Running) return;

        var hosts = new Hosts().Discover();
        DockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");

        if (null != DockerHost)
        {
            if (DockerHost.State != ServiceRunningState.Running) DockerHost.Start();

            return;
        }

        if (hosts.Count > 0) DockerHost = hosts.First();

        if (null != DockerHost) return;

        EnsureDockerHost();
    }
}
public class MyTestFixture : DockerComposeTestBase
{
    public MyTestFixture()
    {
    }

    protected override ICompositeService Build()
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(),
            (TemplateString)"docker-compose.yaml");

        return new DockerComposeCompositeService(
            DockerHost,
            new DockerComposeConfig
            {
                ComposeFilePath = new List<string> { file },
                ForceRecreate = true,
                RemoveOrphans = true,
                StopOnDispose = true
            });
    }
}
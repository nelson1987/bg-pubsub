
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Xunit.Abstractions;

namespace BG.PubSub.IntegrationTests;
public class UnitTest1 : IClassFixture<MyTestFixture>
{
    
    private readonly ITestOutputHelper output;

    public UnitTest1(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async void Test1()
    {

        output.WriteLine($"method: {nameof(Test1)}");
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

        CompositeService = Build();
        try
        {
            CompositeService.Start();
        }
        catch
        {
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
    }

    protected virtual void OnContainerInitialized()
    {
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
using BG.PubSub.Application.Features;
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BG.PubSub.IntegrationTests;
public sealed class RabbitMqContainerTest : IClassFixture<MyTestFixture>
{
    [Fact(DisplayName = "Teste Without container(Mock - TestHarness)")]
    public async Task CriacaoApi()
    {
        await using var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                        services.AddMassTransitTestHarness()));
        var testHarness = application.Services.GetTestHarness();
        using var client = application.CreateClient();
        var nome = "nelson";
        await client.PostAsync($"/evento?nome={nome}", null);
        var consumerTest = testHarness.GetConsumerHarness<CriaAlunoConsumer>();
        Assert.True(await consumerTest.Consumed.Any<CriaAlunoEvent>(x => x.Context.Message.Nome == nome));
    }

    [Fact(DisplayName = "Teste With container")]
    public void ConsumeMessageFromQueue()
    {
        const string queue = "hello";

        const string message = "Hello World!";

        string? actualMessage = null;

        // Signal the completion of message reception.
        EventWaitHandle waitHandle = new ManualResetEvent(false);

        // Create and establish a connection.
        var connectionFactory = new ConnectionFactory()
        {
            Port = 5672,
            Password = "guest",
            UserName = "guest",
            HostName = "localhost",
        };
        //connectionFactory.Uri = new Uri("amqp://guest:guest@localhost:5672");
        using var connection = connectionFactory.CreateConnection();

        // Send a message to the channel.
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue, false, false, false, null);
        channel.BasicPublish(string.Empty, queue, null, Encoding.Default.GetBytes(message));

        // Consume a message from the channel.
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (_, eventArgs) =>
        {
            actualMessage = Encoding.Default.GetString(eventArgs.Body.ToArray());
            waitHandle.Set();
        };

        channel.BasicConsume(queue, true, consumer);
        waitHandle.WaitOne(TimeSpan.FromSeconds(1));

        Assert.Equal(message, actualMessage);
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
        //Time to warm up container
        Thread.Sleep(20000);
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

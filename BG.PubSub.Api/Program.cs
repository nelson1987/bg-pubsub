using BG.PubSub.Api.Events;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICriaAlunoProducer, CriaAlunoProducer>();
builder.Services.AddTransient<ICriaAlunoConsumer, CriaAlunoConsumer>();
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<CriaAlunoConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.MapPost("/evento", async (string nome,
    [FromServices] ICriaAlunoProducer producer) =>
{
    var evento = new CriaAlunoEvent() { Nome = nome };
    await producer.Send(evento);
})
.WithName("PublicaEvento")
.WithOpenApi();

app.Run();

public partial class Program { }
public interface ICriaAlunoProducer
{
    Task Send(CriaAlunoEvent @event);
}
public interface ICriaAlunoConsumer : 
IConsumer<CriaAlunoEvent> {}
public class CriaAlunoProducer : ICriaAlunoProducer
{
    private readonly IBus _producer;
    private readonly ILogger<CriaAlunoProducer> _logger;

    public CriaAlunoProducer(IBus producer, ILogger<CriaAlunoProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public Task Send(CriaAlunoEvent @event)
    {
        _producer.Publish(@event);
        _logger.LogInformation($"Mensagem Produzida {nameof(@event)}.");
        return Task.CompletedTask;
    }
}

public class CriaAlunoConsumer :
    ICriaAlunoConsumer
{
    private readonly ILogger<CriaAlunoConsumer> _logger;

    public CriaAlunoConsumer(ILogger<CriaAlunoConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CriaAlunoEvent> context)
    {
        var message = context.Message;
        await Console.Out.WriteLineAsync($"Message from Producer : {message.Nome}");
        _logger.LogInformation($"Mensagem Consumida {message}.");
    }
}

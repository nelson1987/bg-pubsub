using BG.PubSub.Api.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICriaAlunoProducer, CriaAlunoProducer>();
builder.Services.AddTransient<ICommandHandler<CriaAlunoCommand>, CriaAlunoCommandHandler>();
builder.Services.AddTransient<IEventHandler<CriaAlunoEvent>, CriaAlunoEventHandler>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
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
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<CriaAlunoCommand> handler) =>
{
    var evento = new CriaAlunoCommand() { Nome = nome };
    await handler.Handle(evento, cancellationToken);
})
.WithName("PublicaEvento")
.WithOpenApi();

app.Run();

public partial class Program { }
public interface ICommand
{
}
public interface IEvent
{
}
public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent consumer);
}
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}
public interface ICriaAlunoProducer
{
    Task Send(CriaAlunoEvent @event);
}
public interface IAlunoRepository
{
    Task Insert(CriaAlunoEvent @event);
}
public class AlunoRepository : IAlunoRepository
{
    public async Task Insert(CriaAlunoEvent @event)
    {
        await Console.Out.WriteLineAsync($"Event from Repository : {@event.Nome}");
    }
}
public class CriaAlunoCommand : ICommand
{
    public Guid Id = Guid.NewGuid();
    public required string Nome { get; set; }
}
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
public class CriaAlunoConsumer : IConsumer<CriaAlunoEvent>
{
    private readonly ILogger<CriaAlunoConsumer> _logger;
    private readonly IEventHandler<CriaAlunoEvent> _handler;
    public CriaAlunoConsumer(ILogger<CriaAlunoConsumer> logger, IEventHandler<CriaAlunoEvent> handler)
    {
        _logger = logger;
        _handler = handler;
    }

    public async Task Consume(ConsumeContext<CriaAlunoEvent> context)
    {
        var message = context.Message;
        await Console.Out.WriteLineAsync($"Message from Producer : {message.Nome}");
        _logger.LogInformation($"Mensagem Consumida {message}.");
        await _handler.Handle(message);
    }
}
public class CriaAlunoCommandHandler : ICommandHandler<CriaAlunoCommand>
{
    private readonly ICriaAlunoProducer _producer;

    public CriaAlunoCommandHandler(ICriaAlunoProducer producer)
    {
        _producer = producer;
    }

    public async Task Handle(CriaAlunoCommand command, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"Command from CommandHandler : {command.Nome}");
        var evento = new CriaAlunoEvent() { Nome = command.Nome };
        await _producer.Send(evento);
    }
}
public class CriaAlunoEventHandler : IEventHandler<CriaAlunoEvent>
{
    private readonly IAlunoRepository? _repository;
    public async Task Handle(CriaAlunoEvent consumer)
    {
        await Console.Out.WriteLineAsync($"Message from EventHandler : {consumer.Nome}");
        //_logger.LogInformation($"Mensagem Consumida {consumer}.");
    }
}

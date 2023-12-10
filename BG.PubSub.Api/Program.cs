using BG.PubSub.Api.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICriaAlunoProducer, CriaAlunoProducer>();
builder.Services.AddTransient<ICommandHandler<CriaAlunoCommand>, CriaAlunoCommandHandler>();
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
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/evento", async (string nome,
    CancellationToken cancellationToken,
    [FromServices] ICommandHandler<CriaAlunoCommand> producer) =>
{
    var evento = new CriaAlunoCommand() { Nome = nome };
    await producer.Handle(evento, cancellationToken);
})
.WithName("PublicaEvento")
.WithOpenApi();

app.Run();

public interface ICriaAlunoProducer
{
    Task Send(CriaAlunoEvent @event);
}
public interface ICommand
{
}
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}
public interface IAlunoRepository
{
    Task Insert(CriaAlunoEvent @event);
}

public class CriaAlunoCommand : ICommand
{
    public Guid Id = Guid.NewGuid();
    public required string Nome { get; set; }
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
        var evento = new CriaAlunoEvent() { Nome = command.Nome };
        await _producer.Send(evento);
    }
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
    private readonly IAlunoRepository _repository;
    public CriaAlunoConsumer(ILogger<CriaAlunoConsumer> logger, IAlunoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CriaAlunoEvent> context)
    {
        var message = context.Message;
        await _repository.Insert(message);
        await Console.Out.WriteLineAsync($"Message from Producer : {message.Nome}");
        _logger.LogInformation($"Mensagem Consumida {message}.");
    }
}

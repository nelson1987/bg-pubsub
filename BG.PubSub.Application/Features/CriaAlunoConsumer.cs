using BG.PubSub.Application.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BG.PubSub.Application.Features;

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
        _logger.LogInformation($"Mensagem Consumida {message.ToJson()}.");
        await _handler.Handle(message);
    }
}
public class CriaContaConsumer : IConsumer<CriaContaEvent>
{
    private readonly ILogger<CriaContaConsumer> _logger;
    private readonly IEventHandler<CriaContaEvent> _handler;
    private readonly IContaRepository _repository;
    public CriaContaConsumer(ILogger<CriaContaConsumer> logger, IEventHandler<CriaContaEvent> handler, IContaRepository repository)
    {
        _logger = logger;
        _handler = handler;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CriaContaEvent> context)
    {
        var message = context.Message;
        await Console.Out.WriteLineAsync($"Message from Producer : {message.Nome}");
        _logger.LogInformation($"Mensagem Consumida {message.ToJson()}.");
        await _repository.Incluir(new Entities.Conta() { Nome = message.Nome }, CancellationToken.None);
        await _handler.Handle(message);
    }
}
public static class Extensions
{
    public static string ToJson(this object model)
    {
        return JsonSerializer.Serialize(model);
    }
    //public static T MapTo<T>(this object model)
    //{
    //    //return JsonSerializer.Serialize(model);
    //}
}

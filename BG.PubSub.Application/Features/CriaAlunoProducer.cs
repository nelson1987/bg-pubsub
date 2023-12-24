using BG.PubSub.Application.Abstractions;
using FluentResults;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BG.PubSub.Application.Features;

public class CriaAlunoProducer : ICriaAlunoProducer
{
    private readonly IBus _producer;
    private readonly ILogger<CriaAlunoProducer> _logger;

    public CriaAlunoProducer(IBus producer, ILogger<CriaAlunoProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task<Result> Send(CriaAlunoEvent @event)
    {
        await _producer.Publish(@event);
        _logger.LogInformation($"Mensagem Produzida {nameof(@event)}.");
        return Result.Ok();
    }
}

public class CriaContaProducer : ICriaContaProducer
{
    private readonly IBus _producer;
    private readonly ILogger<CriaContaProducer> _logger;

    public CriaContaProducer(IBus producer, ILogger<CriaContaProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task<Result> Send(CriaContaEvent @event)
    {
        await _producer.Publish(@event);
        _logger.LogInformation($"Mensagem Produzida {nameof(@event)}.");
        return Result.Ok();
    }
}

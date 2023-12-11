using BG.PubSub.Application.Abstractions;
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

    public Task Send(CriaAlunoEvent @event)
    {
        _producer.Publish(@event);
        _logger.LogInformation($"Mensagem Produzida {nameof(@event)}.");
        return Task.CompletedTask;
    }
}

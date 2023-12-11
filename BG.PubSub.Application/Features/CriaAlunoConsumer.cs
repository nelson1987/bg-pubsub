using BG.PubSub.Application.Abstractions;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BG.PubSub.Application.Features;

public class CriaAlunoConsumer : IConsumer<CriaAlunoEvent>
{
    private readonly ILogger<CriaAlunoConsumer> _logger;
    private readonly IEventHandler<CriaAlunoEvent> _handler;
    private readonly IAlunoRepository _repository;
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

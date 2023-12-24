using BG.PubSub.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace BG.PubSub.Application.Features;

public class CriaAlunoEventHandler : IEventHandler<CriaAlunoEvent>
{
    private readonly IAlunoRepository? _repository;
    public async Task Handle(CriaAlunoEvent consumer)
    {
        await Console.Out.WriteLineAsync($"Message from EventHandler : {consumer.Nome}");
        //_logger.LogInformation($"Mensagem Consumida {consumer}.");
    }
}
public class CriaContaEventHandler : IEventHandler<CriaContaEvent>
{
    private readonly ILogger<CriaContaEventHandler> _logger;
    private readonly IContaRepository _repository;

    public CriaContaEventHandler(ILogger<CriaContaEventHandler> logger, IContaRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Handle(CriaContaEvent consumer)
    {
        await Console.Out.WriteLineAsync($"Message from EventHandler : {consumer.Nome}");
        _logger.LogInformation("Inserir na base");
        await _repository.Incluir(new Entities.Conta() { Nome = consumer.Nome }, CancellationToken.None);
    }
}
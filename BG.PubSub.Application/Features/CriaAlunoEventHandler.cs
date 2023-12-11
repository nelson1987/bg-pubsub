using BG.PubSub.Application.Abstractions;

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
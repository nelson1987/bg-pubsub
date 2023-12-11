using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features;

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

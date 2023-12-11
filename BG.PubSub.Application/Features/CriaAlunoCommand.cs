using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features;

public record CriaAlunoCommand(string Nome) : ICommand
{
    public Guid Id = Guid.NewGuid();
}

using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features;
public class CriaAlunoEvent : IEvent
{
    public Guid Id = Guid.NewGuid();
    public required string Nome { get; set; }
};

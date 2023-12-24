using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features;

public class CriaAlunoEvent : IEvent
{
    public required string Nome { get; set; }
}

public class CriaContaEvent : IEvent
{
    public required string Nome { get; set; }
    public required string Documento { get; set; }
}
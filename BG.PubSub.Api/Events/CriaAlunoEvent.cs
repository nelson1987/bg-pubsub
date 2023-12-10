namespace BG.PubSub.Api.Events;
public class CriaAlunoEvent : IEvent
{
    public Guid Id = Guid.NewGuid();
    public required string Nome { get; set; }
};
namespace BG.PubSub.Api.Events;
public class CriaAlunoEvent
{
    public Guid Id = Guid.NewGuid();
    public required string Nome { get; set; }
};

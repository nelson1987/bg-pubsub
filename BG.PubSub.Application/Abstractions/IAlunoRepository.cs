using BG.PubSub.Application.Features;

namespace BG.PubSub.Application.Abstractions
{
    public interface IAlunoRepository
    {
        Task Insert(CriaAlunoEvent @event);
    }
}

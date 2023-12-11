using BG.PubSub.Application.Features;

namespace BG.PubSub.Application.Abstractions
{
    public interface ICriaAlunoProducer
    {
        Task Send(CriaAlunoEvent @event);
    }
}

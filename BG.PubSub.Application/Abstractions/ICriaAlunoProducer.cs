using BG.PubSub.Application.Features;
using FluentResults;

namespace BG.PubSub.Application.Abstractions
{
    public interface ICriaAlunoProducer
    {
        Task<Result> Send(CriaAlunoEvent @event);
    }
}

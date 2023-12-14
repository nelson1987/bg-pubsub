using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public class AlteraLimiteCommandHandler : ICommandHandler<AlteraLimiteCommand>
{
    public Task<Result> Handle(AlteraLimiteCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public class AlterarLimiteCommandHandler : ICommandHandler<AlterarLimiteCommand>
{
    public Task<Result> Handle(AlterarLimiteCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

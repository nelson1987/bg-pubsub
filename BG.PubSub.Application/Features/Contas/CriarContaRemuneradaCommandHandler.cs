using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class CriarContaRemuneradaCommandHandler : ICommandHandler<CriarContaRemuneradaCommand>
{
    public Task<Result> Handle(CriarContaRemuneradaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

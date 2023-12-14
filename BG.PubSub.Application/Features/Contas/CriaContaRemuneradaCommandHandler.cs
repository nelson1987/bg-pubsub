using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class CriaContaRemuneradaCommandHandler : ICommandHandler<CriaContaRemuneradaCommand>
{
    public Task<Result> Handle(CriaContaRemuneradaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

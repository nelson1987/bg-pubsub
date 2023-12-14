using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public class SolicitaCartaoCreditoCommandHandler : ICommandHandler<SolicitaCartaoCreditoCommand>
{
    public Task<Result> Handle(SolicitaCartaoCreditoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

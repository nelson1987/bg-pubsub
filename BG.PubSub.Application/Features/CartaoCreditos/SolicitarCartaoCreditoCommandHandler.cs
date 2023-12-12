using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public class SolicitarCartaoCreditoCommandHandler : ICommandHandler<SolicitarCartaoCreditoCommand>
{
    public Task<Result> Handle(SolicitarCartaoCreditoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

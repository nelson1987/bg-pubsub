using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Investimento;

public class RealizarInvestimentoCommandHandler : ICommandHandler<RealizarInvestimentoCommand>
{
    public Task<Result> Handle(RealizarInvestimentoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

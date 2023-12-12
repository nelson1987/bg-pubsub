using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Transacoes;

public class RealizaCreditoCommandHandler : ICommandHandler<RealizaCreditoCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizaCreditoCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizaCreditoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

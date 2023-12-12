using BG.PubSub.Application.Abstractions;
using FluentResults;
using BG.PubSub.Application.Entities;

namespace BG.PubSub.Application.Features.Investimento;

public class RealizarInvestimentoCommandHandler : ICommandHandler<RealizarInvestimentoCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizarInvestimentoCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizarInvestimentoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        return Result.Ok();
    }
}

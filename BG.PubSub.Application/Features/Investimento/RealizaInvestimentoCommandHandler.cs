using BG.PubSub.Application.Abstractions;
using FluentResults;
using BG.PubSub.Application.Entities;

namespace BG.PubSub.Application.Features.Investimento;

public class RealizaInvestimentoCommandHandler : ICommandHandler<RealizaInvestimentoCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizaInvestimentoCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizaInvestimentoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        return Result.Ok();
    }
}

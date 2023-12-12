using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Transacoes;

public class RealizaTransferenciaCommandHandler : ICommandHandler<RealizaTransferenciaCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizaTransferenciaCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizaTransferenciaCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

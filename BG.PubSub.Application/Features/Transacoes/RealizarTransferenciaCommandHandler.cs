using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Transacoes;

public class RealizarTransferenciaCommandHandler : ICommandHandler<RealizarTransferenciaCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizarTransferenciaCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizarTransferenciaCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

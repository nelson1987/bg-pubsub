using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Transacoes;

public class RealizarDebitoCommandHandler : ICommandHandler<RealizarDebitoCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizarDebitoCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizarDebitoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

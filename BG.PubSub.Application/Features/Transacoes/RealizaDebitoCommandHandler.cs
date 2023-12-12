using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Transacoes;

public class RealizaDebitoCommandHandler : ICommandHandler<RealizaDebitoCommand>
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RealizaDebitoCommandHandler(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<Result> Handle(RealizaDebitoCommand command, CancellationToken cancellationToken)
    {
        await _transacaoRepository.Incluir(new Transacao(), cancellationToken);
        throw new NotImplementedException();
    }
}

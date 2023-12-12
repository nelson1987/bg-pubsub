using BG.PubSub.Application.Entities;

namespace BG.PubSub.Application.Abstractions;
public interface ITransacaoRepository
{
    Task Incluir(Transacao transacao, CancellationToken cancellationToken);
}

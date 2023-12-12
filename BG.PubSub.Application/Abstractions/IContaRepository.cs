using BG.PubSub.Application.Entities;

namespace BG.PubSub.Application.Abstractions;

public interface IContaRepository
{
    Task<Guid?> Incluir(Conta conta, CancellationToken cancellationToken);
    Task<Guid?> Atualizar(Conta conta, CancellationToken cancellationToken);
    Task<Guid?> Excluir(Conta conta, CancellationToken cancellationToken);
}

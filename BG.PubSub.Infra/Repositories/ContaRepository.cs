using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;

namespace BG.PubSub.Infra.Repositories;

public class ContaRepository : IContaRepository
{
    public Task<Guid?> Atualizar(Conta conta, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid?> Excluir(Conta conta, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid?> Incluir(Conta conta, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"Event from Repository : {conta.Nome}");
        return Guid.NewGuid();
    }
}

using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ListaContasQueryHandler : IQueryHandler<ListaContasQuery>
{
    public Task<Result> Handle(ListaContasQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

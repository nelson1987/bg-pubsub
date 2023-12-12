using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ListarContasQueryHandler : IQueryHandler<ListarContasQuery>
{
    public Task<Result> Handle(ListarContasQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

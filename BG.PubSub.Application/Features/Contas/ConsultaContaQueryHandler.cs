using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ConsultaContaQueryHandler : IQueryHandler<ConsultaContaQuery>
{
    public Task<Result> Handle(ConsultaContaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

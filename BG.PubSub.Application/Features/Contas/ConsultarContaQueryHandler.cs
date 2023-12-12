using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ConsultarContaQueryHandler : IQueryHandler<ConsultarContaQuery>
{
    public Task<Result> Handle(ConsultarContaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

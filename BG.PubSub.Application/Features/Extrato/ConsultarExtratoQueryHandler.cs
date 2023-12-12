using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Extrato;

public class ConsultarExtratoQueryHandler : IQueryHandler<ConsultarExtratoQuery>
{
    public Task<Result> Handle(ConsultarExtratoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Extratos;

public class ConsultaExtratoQueryHandler : IQueryHandler<ConsultaExtratoQuery>
{
    public Task<Result> Handle(ConsultaExtratoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

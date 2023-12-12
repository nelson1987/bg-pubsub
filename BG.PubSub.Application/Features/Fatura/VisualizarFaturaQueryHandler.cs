using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Fatura;

public class VisualizarFaturaQueryHandler : IQueryHandler<VisualizarFaturaQuery>
{
    public Task<Result> Handle(VisualizarFaturaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Faturas;

public class VisualizarFaturaQueryHandler : IQueryHandler<VisualizarFaturaQuery>
{
    public Task<Result> Handle(VisualizarFaturaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
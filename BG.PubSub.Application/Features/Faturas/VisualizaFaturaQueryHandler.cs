using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Faturas;

public class VisualizaFaturaQueryHandler : IQueryHandler<VisualizaFaturaQuery>
{
    public Task<Result> Handle(VisualizaFaturaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
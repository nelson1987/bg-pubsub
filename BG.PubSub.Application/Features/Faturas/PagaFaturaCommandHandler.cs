using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Faturas;

public class PagaFaturaCommandHandler : ICommandHandler<PagaFaturaCommand>
{
    public Task<Result> Handle(PagaFaturaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

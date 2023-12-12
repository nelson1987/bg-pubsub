using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Fatura;

public class PagarFaturaCommandHandler : ICommandHandler<PagarFaturaCommand>
{
    public Task<Result> Handle(PagarFaturaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

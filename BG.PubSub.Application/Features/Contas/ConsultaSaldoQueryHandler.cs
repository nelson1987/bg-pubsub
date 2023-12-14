using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ConsultaSaldoQueryHandler : IQueryHandler<ConsultaSaldoQuery>
{
    public Task<Result> Handle(ConsultaSaldoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

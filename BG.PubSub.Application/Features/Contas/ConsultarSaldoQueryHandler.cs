using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ConsultarSaldoQueryHandler : IQueryHandler<ConsultarSaldoQuery>
{
    public Task<Result> Handle(ConsultarSaldoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

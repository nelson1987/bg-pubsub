using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.ExtratoCartoes;

public class ListarExtratoCartaoCreditoQueryHandler : IQueryHandler<ListarExtratoCartaoCreditoQuery>
{
    public Task<Result> Handle(ListarExtratoCartaoCreditoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

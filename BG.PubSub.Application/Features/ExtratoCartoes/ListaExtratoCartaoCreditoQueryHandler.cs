using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.ExtratoCartoes;

public class ListaExtratoCartaoCreditoQueryHandler : IQueryHandler<ListaExtratoCartaoCreditoQuery>
{
    public Task<Result> Handle(ListaExtratoCartaoCreditoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

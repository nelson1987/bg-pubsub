using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.ExtratoCartao;

public record ListarExtratoCartaoCreditoQuery(string Nome) : IQuery;

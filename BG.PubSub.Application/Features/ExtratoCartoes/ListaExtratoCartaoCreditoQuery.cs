using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.ExtratoCartoes;

public record ListaExtratoCartaoCreditoQuery(string Nome) : IQuery;

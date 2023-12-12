using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Extratos;

public record ConsultaExtratoQuery(string Nome) : IQuery;

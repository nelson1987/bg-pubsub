using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Extrato;

public record ConsultarExtratoQuery(string Nome) : IQuery;

using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Contas;

public record ConsultaContaQuery(string Nome) : IQuery;

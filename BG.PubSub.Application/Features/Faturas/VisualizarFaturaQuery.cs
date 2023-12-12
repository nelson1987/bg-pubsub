using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Faturas;

public record VisualizarFaturaQuery(string Nome) : IQuery;

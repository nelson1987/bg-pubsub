using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Investimento;

public record RealizarInvestimentoCommand(string Nome) : ICommand;

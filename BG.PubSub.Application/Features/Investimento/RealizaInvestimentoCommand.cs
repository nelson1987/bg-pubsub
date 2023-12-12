using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Investimento;

public record RealizaInvestimentoCommand(string Nome) : ICommand;

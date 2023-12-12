using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacoes;

public record RealizaCreditoCommand(string Nome) : ICommand;

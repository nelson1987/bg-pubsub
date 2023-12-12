using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacoes;

public record RealizarCreditoCommand(string Nome) : ICommand;

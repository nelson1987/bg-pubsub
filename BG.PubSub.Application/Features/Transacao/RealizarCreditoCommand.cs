using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacao;

public record RealizarCreditoCommand(string Nome) : ICommand;

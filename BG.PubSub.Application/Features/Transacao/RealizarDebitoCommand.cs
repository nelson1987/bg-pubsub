using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacao;

public record RealizarDebitoCommand(string Nome) : ICommand;

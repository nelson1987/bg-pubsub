using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacoes;

public record RealizarTransferenciaCommand(string Nome) : ICommand;

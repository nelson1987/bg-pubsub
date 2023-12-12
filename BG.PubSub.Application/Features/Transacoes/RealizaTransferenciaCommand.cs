using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Transacoes;

public record RealizaTransferenciaCommand(string Nome) : ICommand;

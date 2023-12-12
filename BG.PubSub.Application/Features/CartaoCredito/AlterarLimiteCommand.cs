using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.CartaoCredito;

public record AlterarLimiteCommand(string Nome) : ICommand;

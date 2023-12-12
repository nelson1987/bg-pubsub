using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public record AlterarLimiteCommand(string Nome) : ICommand;

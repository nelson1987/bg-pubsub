using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Fatura;

public record PagarFaturaCommand(string Nome) : ICommand;

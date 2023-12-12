using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Contas;

public record ExcluirContaCommand(string Nome) : ICommand;

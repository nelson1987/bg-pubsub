using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.CartaoCreditos;

public record SolicitaCartaoCreditoCommand(string Nome) : ICommand;

using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class AtualizarContaCommandHandler : ICommandHandler<AtualizarContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public AtualizarContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(AtualizarContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Atualizar(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

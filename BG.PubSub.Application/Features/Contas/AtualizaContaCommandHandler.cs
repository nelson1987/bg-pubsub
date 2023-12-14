using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class AtualizaContaCommandHandler : ICommandHandler<AtualizaContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public AtualizaContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(AtualizaContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Atualizar(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

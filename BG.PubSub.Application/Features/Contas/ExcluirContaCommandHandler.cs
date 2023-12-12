using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ExcluirContaCommandHandler : ICommandHandler<ExcluirContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public ExcluirContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(ExcluirContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Excluir(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

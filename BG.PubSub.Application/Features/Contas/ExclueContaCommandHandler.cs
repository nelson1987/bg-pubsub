using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class ExclueContaCommandHandler : ICommandHandler<ExclueContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public ExclueContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(ExclueContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Excluir(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

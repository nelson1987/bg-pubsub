using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class CriaContaCommandHandler : ICommandHandler<CriaContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public CriaContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(CriaContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Incluir(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

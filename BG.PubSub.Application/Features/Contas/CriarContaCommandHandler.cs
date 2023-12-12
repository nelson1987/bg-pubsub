using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using FluentResults;

namespace BG.PubSub.Application.Features.Contas;

public class CriarContaCommandHandler : ICommandHandler<CriarContaCommand>
{
    private readonly IContaRepository _contaRepository;

    public CriarContaCommandHandler(IContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<Result> Handle(CriarContaCommand command, CancellationToken cancellationToken)
    {
        Guid? id = await _contaRepository.Incluir(new Conta(), cancellationToken);
        return Result.Ok();
    }
}

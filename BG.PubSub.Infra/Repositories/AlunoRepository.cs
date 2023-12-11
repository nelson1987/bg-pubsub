using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;

namespace BG.PubSub.Infra.Repositories;

public class AlunoRepository : IAlunoRepository
{
    public async Task<Guid?> Incluir(Aluno aluno)
    {
        await Console.Out.WriteLineAsync($"Event from Repository : {aluno.Nome}");
        return Guid.NewGuid();
    }
}

using BG.PubSub.Application.Entities;

namespace BG.PubSub.Application.Abstractions;

public interface IAlunoRepository
{
    Task<Guid?> Incluir(Aluno aluno);
}

using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Features;

namespace BG.PubSub.Infra.Repositories;

public class AlunoRepository : IAlunoRepository
{
    public async Task Insert(CriaAlunoEvent @event)
    {
        await Console.Out.WriteLineAsync($"Event from Repository : {@event.Nome}");
    }
}

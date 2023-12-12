using BG.PubSub.Application.Abstractions;
using FluentResults;

namespace BG.PubSub.Application.Features.Conta;
public interface IContaRepository
{
    
}
public interface ITransacaoRepository
{

}
public record CriarContaCommand(string Nome) : ICommand;
public class CriarContaCommandHandler : ICommandHandler<CriarContaCommand>
{
    public Task<Result> Handle(CriarContaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record AtualizarContaCommand(string Nome) : ICommand;
public class AtualizarContaCommandHandler : ICommandHandler<AtualizarContaCommand>
{
    public Task<Result> Handle(AtualizarContaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ExcluirContaCommand(string Nome) : ICommand;
public class ExcluirContaCommandHandler : ICommandHandler<ExcluirContaCommand>
{
    public Task<Result> Handle(ExcluirContaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record CriarContaRemuneradaCommand(string Nome) : ICommand;
public class CriarContaRemuneradaCommandHandler : ICommandHandler<CriarContaRemuneradaCommand>
{
    public Task<Result> Handle(CriarContaRemuneradaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record RealizarDebitoCommand(string Nome) : ICommand;
public class RealizarDebitoCommandHandler : ICommandHandler<RealizarDebitoCommand>
{
    public Task<Result> Handle(RealizarDebitoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record RealizarCreditoCommand(string Nome) : ICommand;
public class RealizarCreditoCommandHandler : ICommandHandler<RealizarCreditoCommand>
{
    public Task<Result> Handle(RealizarCreditoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record RealizarTransferenciaCommand(string Nome) : ICommand;
public class RealizarTransferenciaCommandHandler : ICommandHandler<RealizarTransferenciaCommand>
{
    public Task<Result> Handle(RealizarTransferenciaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record RealizarInvestimentoCommand(string Nome) : ICommand;
public class RealizarInvestimentoCommandHandler : ICommandHandler<RealizarInvestimentoCommand>
{
    public Task<Result> Handle(RealizarInvestimentoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record SolicitarCartaoCreditoCommand(string Nome) : ICommand;
public class SolicitarCartaoCreditoCommandHandler : ICommandHandler<SolicitarCartaoCreditoCommand>
{
    public Task<Result> Handle(SolicitarCartaoCreditoCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record AlterarLimiteCommand(string Nome) : ICommand;
public class AlterarLimiteCommandHandler : ICommandHandler<AlterarLimiteCommand>
{
    public Task<Result> Handle(AlterarLimiteCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record PagarFaturaCommand(string Nome) : ICommand;
public class PagarFaturaCommandHandler : ICommandHandler<PagarFaturaCommand>
{
    public Task<Result> Handle(PagarFaturaCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ConsultarContaQuery(string Nome) : IQuery;
public class ConsultarContaQueryHandler : IQueryHandler<ConsultarContaQuery>
{
    public Task<Result> Handle(ConsultarContaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ListarContasQuery(string Nome) : IQuery;
public class ListarContasQueryHandler : IQueryHandler<ListarContasQuery>
{
    public Task<Result> Handle(ListarContasQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ConsultarSaldoQuery(string Nome) : IQuery;
public class ConsultarSaldoQueryHandler : IQueryHandler<ConsultarSaldoQuery>
{
    public Task<Result> Handle(ConsultarSaldoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ConsultarExtratoQuery(string Nome) : IQuery;
public class ConsultarExtratoQueryHandler : IQueryHandler<ConsultarExtratoQuery>
{
    public Task<Result> Handle(ConsultarExtratoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record ListarExtratoCartaoCreditoQuery(string Nome) : IQuery;
public class ListarExtratoCartaoCreditoQueryHandler : IQueryHandler<ListarExtratoCartaoCreditoQuery>
{
    public Task<Result> Handle(ListarExtratoCartaoCreditoQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public record VisualizarFaturaQuery(string Nome) : IQuery;
public class VisualizarFaturaQueryHandler : IQueryHandler<VisualizarFaturaQuery>
{
    public Task<Result> Handle(VisualizarFaturaQuery command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
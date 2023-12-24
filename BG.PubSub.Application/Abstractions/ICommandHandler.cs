using FluentResults;

namespace BG.PubSub.Application.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}
public interface IQueryHandler<TQuery> where TQuery : IQuery
{
    Task<Result> Handle(TQuery command, CancellationToken cancellationToken);
}
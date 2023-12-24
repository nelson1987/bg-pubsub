using BG.PubSub.Application.Abstractions;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace BG.PubSub.Application.Features;

public class CriaAlunoCommandHandler : ICommandHandler<CriaAlunoCommand>
{
    private readonly ICriaAlunoProducer _producer;
    private readonly IValidator<CriaAlunoCommand> _requestValidator;

    public CriaAlunoCommandHandler(ICriaAlunoProducer producer,
            IValidator<CriaAlunoCommand> requestValidator)
    {
        _producer = producer;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CriaAlunoCommand command, CancellationToken cancellationToken)
    {
        var validation = await _requestValidator.ValidateAsync(command, cancellationToken);
        if (!validation.IsValid)
        {
            return validation.ToFailResult();
        }
        await Console.Out.WriteLineAsync($"Command from CommandHandler : {command.Nome}");
        var evento = new CriaAlunoEvent() { Nome = command.Nome };
        var eventoProduzido = await _producer.Send(evento);// command.MapTo<CriaAlunoEvent>());
        if (eventoProduzido!.IsFailed)
            return eventoProduzido;
        /*
        var evento = await _producer.Send(new CriaMatriculaAlunoEvent(command.Cpf, command.Turma));
if (evento!.IsFailed)
    return evento;

Guid? id = await _repository.Incluir(new Matricula() { Cpf = command.Cpf, Turma = command.Turma });
if (id is null)
    return Result.Fail("Erro ao incluir matricula");
*/
        return Result.Ok();
    }
}
public static class FluentResultsExtensions
{
    public static Result ToFailResult(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(x => new FluentResults.Error(x.ErrorMessage)
            .WithMetadata(nameof(x.PropertyName), x.PropertyName)
            .WithMetadata(nameof(x.AttemptedValue), x.AttemptedValue));

        return Result.Fail(errors);
    }
}

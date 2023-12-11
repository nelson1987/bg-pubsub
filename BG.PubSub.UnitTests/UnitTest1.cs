using AutoFixture;
using AutoFixture.AutoMoq;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;

namespace BG.PubSub.UnitTests;

public class MatriculaAlunoTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    private readonly MatriculaAlunoCommand _command;
    private readonly IValidator<MatriculaAlunoCommand> _validator;
    private readonly MatriculaAlunoCommandHandler _handler;

    public MatriculaAlunoTests()
    {
        _fixture.Freeze<Mock<IMatriculaRepository>>();
        _fixture.Freeze<Mock<IProducer>>();
        _command = _fixture.Build<MatriculaAlunoCommand>()
            .With(x => x.Cpf, "CPF")
            .With(x => x.Turma, "TURMA")
            .Create();
        _fixture.Freeze<Mock<IMatriculaRepository>>()
                .Setup(x => x.Incluir(It.IsAny<Matricula>()))
                .ReturnsAsync(Guid.NewGuid());
        _fixture.Freeze<Mock<IProducer>>()
                .Setup(x => x.Send(It.IsAny<CriaMatriculaAlunoEvent>()))
                .ReturnsAsync(Result.Ok());
        _fixture.Freeze<Mock<IValidator<MatriculaAlunoCommand>>>()
                .Setup(x => x.Validate(_command))
                .Returns(new ValidationResult());
        _handler = _fixture.Create<MatriculaAlunoCommandHandler>();
        _validator = _fixture.Create<MatriculaAlunoCommandValidator>();
    }

    [Fact]
    public async Task MatricularAluno_ComSucesso()
    {
        var result = await _handler.Handle(_command);

        _fixture.Freeze<Mock<IValidator<MatriculaAlunoCommand>>>()
                .Verify(x => x.Validate(_command)
                , Times.Once);
        _fixture.Freeze<Mock<IMatriculaRepository>>()
                .Verify(x => x.Incluir(It.IsAny<Matricula>())
                , Times.Once);
        _fixture.Freeze<Mock<IProducer>>()
                .Verify(x => x.Send(It.IsAny<CriaMatriculaAlunoEvent>())
                , Times.Once);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task MatricularAluno_ComDocumentoInvalido()
    {
        var matricula = _command with { Turma = string.Empty };
        var result = await _handler.Handle(matricula);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task MatricularAluno_ComTurmaInvalido()
    {
        var matricula = _command with { Cpf = string.Empty };
        var result = await _handler.Handle(matricula);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task MatricularAluno_ComRepositoryRetornandoErro()
    {
        _fixture.Freeze<Mock<IMatriculaRepository>>()
                .Setup(x => x.Incluir(It.IsAny<Matricula>()))
                .ReturnsAsync(null as Guid?);

        var result = await _handler.Handle(_command);

        _fixture.Freeze<Mock<IMatriculaRepository>>()
                .Verify(x => x.Incluir(It.IsAny<Matricula>())
                , Times.Once);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task MatricularAluno_ComErro()
    {
        _fixture.Freeze<Mock<IProducer>>()
                .Setup(x => x.Send(It.IsAny<CriaMatriculaAlunoEvent>()))
                .ReturnsAsync(Result.Fail("Erro ao enviar matricula"));

        var result = await _handler.Handle(_command);

        _fixture.Freeze<Mock<IProducer>>()
                .Verify(x => x.Send(It.IsAny<CriaMatriculaAlunoEvent>())
                , Times.Once);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Given_a_valid_event_when_all_fields_are_valid_should_pass_validation()
    => _validator
            .TestValidate(_command)
            .ShouldNotHaveAnyValidationErrors();
    [Fact]
    public void Given_a_cancellation_request_with_invalid_cpf_should_fail_validation()
    => _validator
        .TestValidate(_command with { Cpf = string.Empty })
        .ShouldHaveValidationErrorFor(x => x.Cpf)
        .Only();

    [Fact]
    public void Given_a_cancellation_request_with_invalid_turma_should_fail_validation()
    => _validator
        .TestValidate(_command with { Turma = string.Empty })
        .ShouldHaveValidationErrorFor(x => x.Turma)
        .Only();
}

public record MatriculaAlunoCommand(string Cpf, string Turma)
{
    internal bool IsValid()
    {
        if (string.IsNullOrEmpty(Cpf))
            return false;
        if (string.IsNullOrEmpty(Turma))
            return false;
        return true;
    }
}

public class MatriculaAlunoCommandHandler
{
    private readonly IMatriculaRepository _repository;
    private readonly IProducer _producer;
    private readonly IValidator<MatriculaAlunoCommand> _requestValidator;

    public MatriculaAlunoCommandHandler(IMatriculaRepository repository, IProducer producer, IValidator<MatriculaAlunoCommand> requestValidator)
    {
        _repository = repository;
        _producer = producer;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(MatriculaAlunoCommand command)
    {
        var validation = _requestValidator.Validate(command);
        if (!validation.IsValid)
        {
            return validation.ToFailResult();
        }

        var evento = await _producer.Send(new CriaMatriculaAlunoEvent(command.Cpf, command.Turma));
        if (evento!.IsFailed)
            return evento;

        Guid? id = await _repository.Incluir(new Matricula() { Cpf = command.Cpf, Turma = command.Turma });
        if (id is null)
            return Result.Fail("Erro ao incluir matricula");

        return Result.Ok();
    }
}

public interface IProducer
{
    Task<Result> Send(CriaMatriculaAlunoEvent @event);
}

public record CriaMatriculaAlunoEvent(string Cpf, string Turma);

public interface IMatriculaRepository
{
    Task<Guid?> Incluir(Matricula matricula);
}

public class Matricula
{
    public required string Cpf { get; set; }
    public required string Turma { get; set; }
}

public class MatriculaAlunoCommandValidator : AbstractValidator<MatriculaAlunoCommand>
{
    public MatriculaAlunoCommandValidator()
    {
        RuleFor(x => x.Cpf)
             .NotEmpty();
        RuleFor(x => x.Turma)
             .NotEmpty();
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

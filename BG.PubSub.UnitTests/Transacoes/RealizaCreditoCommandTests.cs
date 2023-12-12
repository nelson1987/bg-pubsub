using AutoFixture;
using AutoFixture.AutoMoq;
using BG.PubSub.Application.Abstractions;
using BG.PubSub.Application.Entities;
using BG.PubSub.Application.Features;
using BG.PubSub.Application.Features.Transacoes;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;

namespace BG.PubSub.UnitTests;

public class RealizaCreditoCommandTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    private readonly RealizaCreditoCommand _command;
    private readonly IValidator<RealizaCreditoCommand> _validator;
    private readonly RealizaCreditoCommandHandler _handler;
    private readonly CancellationToken _token = CancellationToken.None;

    public RealizaCreditoCommandTests()
    {
        _fixture.Freeze<Mock<IAlunoRepository>>();
        _fixture.Freeze<Mock<ICriaAlunoProducer>>();
        _command = _fixture.Build<RealizaCreditoCommand>()
            .With(x => x.Nome, "NOME")
            .Create();
        _fixture.Freeze<Mock<IAlunoRepository>>()
                .Setup(x => x.Incluir(It.IsAny<Aluno>()))
                .ReturnsAsync(Guid.NewGuid());
        _fixture.Freeze<Mock<ICriaAlunoProducer>>()
                .Setup(x => x.Send(It.IsAny<CriaAlunoEvent>()))
                .ReturnsAsync(Result.Ok());
        _fixture.Freeze<Mock<IValidator<RealizaCreditoCommand>>>()
                .Setup(x => x.Validate(_command))
                .Returns(new ValidationResult());
        _handler = _fixture.Create<RealizaCreditoCommandHandler>();
        _validator = _fixture.Create<RealizaCreditoCommandValidator>();
    }

    [Fact]
    public async Task MatricularAluno_ComSucesso()
    {
        var result = await _handler.Handle(_command, _token);

        _fixture.Freeze<Mock<IValidator<RealizaCreditoCommand>>>()
                .Verify(x => x.Validate(_command)
                , Times.Once);
        _fixture.Freeze<Mock<ICriaAlunoProducer>>()
                .Verify(x => x.Send(It.IsAny<CriaAlunoEvent>())
                , Times.Once);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task MatricularAluno_ComNomeInvalido()
    {
        var matricula = _command with { Nome = string.Empty };
        var result = await _handler.Handle(matricula, _token);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task MatricularAluno_ComErro()
    {
        _fixture.Freeze<Mock<ICriaAlunoProducer>>()
                .Setup(x => x.Send(It.IsAny<CriaAlunoEvent>()))
                .ReturnsAsync(Result.Fail("Erro ao enviar matricula"));

        var result = await _handler.Handle(_command, _token);

        _fixture.Freeze<Mock<ICriaAlunoProducer>>()
                .Verify(x => x.Send(It.IsAny<CriaAlunoEvent>())
                , Times.Once);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Given_a_valid_event_when_all_fields_are_valid_should_pass_validation()
    => _validator
            .TestValidate(_command)
            .ShouldNotHaveAnyValidationErrors();

    [Fact]
    public void Given_a_cancellation_request_with_invalid_nome_should_fail_validation()
    => _validator
        .TestValidate(_command with { Nome = string.Empty })
        .ShouldHaveValidationErrorFor(x => x.Nome)
        .Only();
}
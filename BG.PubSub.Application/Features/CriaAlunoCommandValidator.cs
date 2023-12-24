using FluentValidation;

namespace BG.PubSub.Application.Features;

public class CriaAlunoCommandValidator : AbstractValidator<CriaAlunoCommand>
{
    public CriaAlunoCommandValidator()
    {
        RuleFor(x => x.Nome)
             .NotEmpty();
    }
}

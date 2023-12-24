using FluentValidation;

namespace BG.PubSub.Application.Features.Contas;

public class CriaContaCommandValidator : AbstractValidator<CriaContaCommand>
{
    public CriaContaCommandValidator()
    {
        RuleFor(x => x.Nome)
             .NotEmpty();
    }
}

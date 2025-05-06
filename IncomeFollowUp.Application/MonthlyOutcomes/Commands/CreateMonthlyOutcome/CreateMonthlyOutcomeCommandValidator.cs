using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.CreateMonthlyOutcome;

public class CreateMonthlyOutcomeCommandValidator : AbstractValidator<CreateMonthlyOutcomeCommand>
{
    public CreateMonthlyOutcomeCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualToWithMessage(0);
     
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
    }
}
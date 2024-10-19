using FluentValidation;

namespace IncomeFollowUp.Application.Common.Validators;

public class MonthValidator : AbstractValidator<int>
{
    public MonthValidator()
    {
        RuleFor(x => x)
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");
    }
}
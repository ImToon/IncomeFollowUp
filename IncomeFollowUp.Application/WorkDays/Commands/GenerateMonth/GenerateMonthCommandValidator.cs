using FluentValidation;
using IncomeFollowUp.Application.Common.Validators;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;

public class GenerateMonthCommandValidator : AbstractValidator<GenerateMonthCommand>
{
    public GenerateMonthCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                return !await dbContext.WorkDays.AnyAsync(wd => wd.Date.Month == x.Month && wd.Date.Year == x.Year, cancellationToken);
            })
            .WithMessage("This month has already been generated.");

        RuleFor(x => x.Month)
            .SetValidator(new MonthValidator());

        RuleFor(x => x)
            .SetValidator(new SettingsMustExistValidator<GenerateMonthCommand>(dbContext));
    }
}
using FluentValidation;
using IncomeFollowUp.Application.Common.Validators;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.DeleteMonth;

public class DeleteMonthCommandValidator : AbstractValidator<DeleteMonthCommand>
{
    public DeleteMonthCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                return await dbContext.WorkDays.AnyAsync(wd => wd.Date.Month == x.Month && wd.Date.Year == x.Year, cancellationToken) &&
                       await dbContext.MonthlyIncomes.AnyAsync(mi => mi.Month == x.Month && mi.Year == x.Year, cancellationToken);
            })
            .WithMessage("This month does not exist.");

        RuleFor(x => x.Month)
            .SetValidator(new MonthValidator());
    }
}
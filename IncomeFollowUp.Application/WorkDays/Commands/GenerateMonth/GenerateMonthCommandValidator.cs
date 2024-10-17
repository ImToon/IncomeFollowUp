using FluentValidation;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;

public class GenerateMonthCommandValidator : AbstractValidator<GenerateMonthCommand>
{
    public GenerateMonthCommandValidator (IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");

        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var settings = await dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
                return settings != null;
            })
            .WithMessage("You must setup settings first.");
    }
}
using FluentValidation;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.DeleteMonthlyOutcome;

public class DeleteMonthlyOutcomeCommandValidator : AbstractValidator<DeleteMonthlyOutcomeCommand>
{
    public DeleteMonthlyOutcomeCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var monthlyOutcome = await dbContext.MonthlyOutcomes.FirstOrDefaultAsync(mo => mo.Id == x.Id, cancellationToken);
                return monthlyOutcome != null;
            })
            .WithMessage("Monthly outcome must exist.");
    }
}
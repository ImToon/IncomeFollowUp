using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.UpdateMonthlyOutcome;

public class UpdateMonthlyOutcomeCommandValidator : AbstractValidator<UpdateMonthlyOutcomeCommand>
{
    public UpdateMonthlyOutcomeCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var monthlyOutcome = await dbContext.MonthlyOutcomes.FirstOrDefaultAsync(mo => mo.Id == x.Id, cancellationToken);
                return monthlyOutcome != null;
            })
            .WithMessage("Monthly outcome must exist.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualToWithMessage(0);
     
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
    }
}
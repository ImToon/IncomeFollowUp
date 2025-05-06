using FluentValidation;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;

public class UpdateWorkDaysCommandValidator : AbstractValidator<UpdateWorkDaysCommand>
{
    public UpdateWorkDaysCommandValidator (IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
        .MustAsync(async (x, cancellationToken) =>
        {
            var workDayIds = x.UpdateWorkDayCommands.Select(c => c.Id).ToList();
            var workDays = await dbContext.WorkDays.Where(wd => workDayIds.Contains(wd.Id)).ToListAsync(cancellationToken);
            return workDays.Count == workDayIds.Count;
        })
        .WithMessage("Some work days are not found.");
    }
}
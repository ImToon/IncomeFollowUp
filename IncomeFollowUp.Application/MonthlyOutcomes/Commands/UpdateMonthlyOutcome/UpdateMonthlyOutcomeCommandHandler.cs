using MediatR;
using IncomeFollowUp.Infrastructure;
using IncomeFollowUp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.UpdateMonthlyOutcome;

public class UpdateMonthlyOutcomeCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<UpdateMonthlyOutcomeCommand, MonthlyOutcome>
{
    public async Task<MonthlyOutcome> Handle(UpdateMonthlyOutcomeCommand request, CancellationToken cancellationToken)
    {
        var monthlyOutcomeToUpdate = await dbContext.MonthlyOutcomes.FirstAsync(mo => mo.Id == request.Id, cancellationToken);

        monthlyOutcomeToUpdate.Update(request.Name, request.Amount);

        await dbContext.SaveChangesAsync(cancellationToken);

        return monthlyOutcomeToUpdate;
    }
}
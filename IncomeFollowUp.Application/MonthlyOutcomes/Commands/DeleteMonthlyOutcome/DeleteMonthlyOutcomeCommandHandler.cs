using MediatR;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Infrastructure;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.DeleteMonthlyOutcome;

public class DeleteMonthlyOutcomeCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<DeleteMonthlyOutcomeCommand>
{
    public async Task Handle(DeleteMonthlyOutcomeCommand request, CancellationToken cancellationToken)
    {
        var monthlyOutcomeToUpdate = await dbContext.MonthlyOutcomes.FirstAsync(mo => mo.Id == request.Id, cancellationToken);

        dbContext.MonthlyOutcomes.Remove(monthlyOutcomeToUpdate);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
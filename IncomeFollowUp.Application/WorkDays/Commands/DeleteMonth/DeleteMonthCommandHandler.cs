using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.DeleteMonth;

public class DeleteMonthCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<DeleteMonthCommand>
{
    public async Task Handle(DeleteMonthCommand request, CancellationToken cancellationToken)
    {
        var workdays = await dbContext.WorkDays
                            .Where(wd => wd.Date.Month == request.Month && wd.Date.Year == request.Year)
                            .ToListAsync(cancellationToken);

        var monthlyIncome = await dbContext.MonthlyIncomes
                                .Where(mi => mi.Month == request.Month && mi.Year == request.Year)
                                .FirstAsync(cancellationToken);

        dbContext.WorkDays.RemoveRange(workdays);
        dbContext.MonthlyIncomes.Remove(monthlyIncome);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
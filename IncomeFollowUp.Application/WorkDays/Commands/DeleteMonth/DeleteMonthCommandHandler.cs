using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Commands.DeleteMonth;

public class DeleteMonthCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<DeleteMonthCommand>
{
    public async Task Handle(DeleteMonthCommand request, CancellationToken cancellationToken)
    {
        var workdays = await dbContext.WorkDays.Where(wd => wd.Date.Month == request.Month && wd.Date.Year == request.Year).ToListAsync(cancellationToken);

        dbContext.WorkDays.RemoveRange(workdays);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
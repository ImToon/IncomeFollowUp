using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetVacation;

public class GetExtraDaysQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetExtraDaysQuery, int>
{
    public async Task<int> Handle(GetExtraDaysQuery request, CancellationToken cancellationToken)
    {
        var minDate = new DateTime(request.Year, 1, 1);
        var maxDate = new DateTime(request.Year + 1, 1, 1).AddDays(-1);
        var settings = await dbContext.Settings.FirstAsync(cancellationToken);
        var workDays = await dbContext.WorkDays.Where(w => w.Date >= minDate && w.Date < maxDate && w.IsWorkDay).ToListAsync(cancellationToken);
        
        return workDays
            .GroupBy(wd => wd.Date.Month)
            .Sum(group => 
            {
                var totalMonth = group.Sum(g => g.DailyRate);
                var excess = totalMonth - settings.ExpectedMonthlyIncome;
                return excess / settings.DailyRate;
            });
    }
}
using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Application.WorkDays.Queries.Shared;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetMonthlyWorkDaysSummaries;

public class GetMonthlyWorkDaysSummariesQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetMonthlyWorkDaysSummariesQuery, MonthlyWorkDaysSummary[]>
{
    public async Task<MonthlyWorkDaysSummary[]> Handle(GetMonthlyWorkDaysSummariesQuery request, CancellationToken cancellationToken)
    {
        var minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths((request.NumberOfLatestMonths - 1) * -1);
        var maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
        var workDays = await dbContext.WorkDays.Where(w => w.Date >= minDate && w.Date < maxDate && w.IsWorkDay).ToArrayAsync(cancellationToken);
        
        return workDays
            .OrderBy(wd => wd.Date)
            .GroupBy(wd => wd.Date.Month)
            .Select(group => new MonthlyWorkDaysSummary
            {
                Month = group.Key,
                Year = group.First().Date.Year,
                Total = group.Sum(g => g.DailyRate)
            })
            .ToArray();
    }
}
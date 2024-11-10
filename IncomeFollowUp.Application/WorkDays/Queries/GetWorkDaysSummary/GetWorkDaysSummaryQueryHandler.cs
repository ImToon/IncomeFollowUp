using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;

public class GetWorkDaysSummaryQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetWorkDaysSummaryQuery, IEnumerable<GetWorkDaysSummaryResponse>>
{
    public async Task<IEnumerable<GetWorkDaysSummaryResponse>> Handle(GetWorkDaysSummaryQuery request, CancellationToken cancellationToken)
    {
        WorkDay[] workDays = [];

        if (request.SummaryType == SummaryType.Monthly)
        {
            var minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-6);
            var maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            workDays = await dbContext.WorkDays.Where(w => w.Date >= minDate && w.Date < maxDate && w.IsWorkDay).ToArrayAsync(cancellationToken);
        }

        if (request.SummaryType == SummaryType.Yearly)
        {
            workDays = await dbContext.WorkDays.Where(w => w.Date.Year == request.Year && w.IsWorkDay).ToArrayAsync(cancellationToken);
        }
        
        return workDays
            .OrderBy(wd => wd.Date)
            .GroupBy(wd => wd.Date.Month)
            .Select(group => new GetWorkDaysSummaryResponse
            {
                Month = group.Key,
                Year = group.First().Date.Year,
                Total = group.Sum(g => g.DailyRate)
            })
            .AsEnumerable();
    }
}
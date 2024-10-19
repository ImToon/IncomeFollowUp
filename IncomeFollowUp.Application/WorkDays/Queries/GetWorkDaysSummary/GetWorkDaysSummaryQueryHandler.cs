using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;

public class GetWorkDaysSummaryQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetWorkDaysSummaryQuery, IEnumerable<GetWorkDaysSummaryResponse>>
{
    public async Task<IEnumerable<GetWorkDaysSummaryResponse>> Handle(GetWorkDaysSummaryQuery request, CancellationToken cancellationToken)
    {
        var minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-7);
        var maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var workDays = await dbContext.WorkDays.Where(w => w.Date >= minDate && w.Date < maxDate).ToListAsync(cancellationToken);

        return workDays
            .OrderBy(wd => wd.Date)
            .GroupBy(wd => wd.Date.Month)
            .Select(group => new GetWorkDaysSummaryResponse(
                Month: group.Key,
                Year: group.First().Date.Year,
                Total: group.Sum(g => g.DailyRate))
            )
            .AsEnumerable();
    }
}

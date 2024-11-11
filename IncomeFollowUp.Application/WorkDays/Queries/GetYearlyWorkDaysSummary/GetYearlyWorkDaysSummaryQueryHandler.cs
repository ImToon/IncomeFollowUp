using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;
using IncomeFollowUp.Application.WorkDays.Queries.Shared;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;

public class GetYearlyWorkDaysSummaryQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetYearlyWorkDaysSummaryQuery, GetYearlyWorkDaysSummaryResponse>
{
    public async Task<GetYearlyWorkDaysSummaryResponse> Handle(GetYearlyWorkDaysSummaryQuery request, CancellationToken cancellationToken)
    {
        WorkDay[] workDays = [];

        workDays = await dbContext.WorkDays.Where(w => w.Date.Year == request.Year && w.IsWorkDay).ToArrayAsync(cancellationToken);
        
        var workDaysMonthSummaries = workDays
            .OrderBy(wd => wd.Date)
            .GroupBy(wd => wd.Date.Month)
            .Select(group => new MonthlyWorkDaysSummary
            {
                Month = group.Key,
                Year = group.First().Date.Year,
                Total = group.Sum(g => g.DailyRate)
            })
            .ToArray();

        var monthlyExpenses = await dbContext.MonthlyOutcomes.SumAsync(mo => mo.Amount, cancellationToken);

        return new GetYearlyWorkDaysSummaryResponse 
        {
            MonthlyWorkDaysSummaries = workDaysMonthSummaries,
            AnnualExpenses = monthlyExpenses * workDaysMonthSummaries.Length
        };
    }
}
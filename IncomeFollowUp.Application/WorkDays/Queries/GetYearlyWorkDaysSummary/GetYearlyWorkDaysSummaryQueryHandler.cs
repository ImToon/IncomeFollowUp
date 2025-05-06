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
        MonthlyIncome[] monthlyIncomes = await dbContext.MonthlyIncomes.Where(mi => mi.Year == request.Year).ToArrayAsync(cancellationToken);
        double annualExpenses = await dbContext.MonthlyOutcomes.SumAsync(mo => mo.Amount, cancellationToken) * monthlyIncomes.Length;

        var workDaysMonthSummaries = monthlyIncomes
            .OrderBy(mi => mi.Month)
            .Select(mi => new MonthlyWorkDaysSummary
            {
                Month = mi.Month,
                Year = mi.Year,
                Total = mi.ActualAmount
            })
            .ToArray();

        var settings = await dbContext.Settings.FirstAsync(cancellationToken);
        var totalWorkingDays = GetWorkingDaysInYear(request.Year);
        var totalAvailableExtraDays = (totalWorkingDays * settings.DailyRate - (12 * settings.ExpectedMonthlyIncome)) / settings.DailyRate;
        var totalUsedExtraDays = monthlyIncomes
            .Sum(mi =>
            {
                var totalExpected = mi.ExpectedAmount;
                var totalMonth = mi.ActualAmount;

                var total = totalExpected > settings.ExpectedMonthlyIncome ? totalExpected : settings.ExpectedMonthlyIncome;

                var outcomeDifference = total - totalMonth;

                return outcomeDifference != 0 ? outcomeDifference / settings.DailyRate : 0;
            });

        var remainingVacation = totalAvailableExtraDays - totalUsedExtraDays;

        return new GetYearlyWorkDaysSummaryResponse
        {
            MonthlyWorkDaysSummaries = workDaysMonthSummaries,
            TotalIncome = monthlyIncomes.Sum(mi => mi.ActualAmount),
            TotalOutcome = annualExpenses,
            RemainingVacation = remainingVacation
        };
    }

    private static int TotalWorkDaysByMonth(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        int workingDays = 0;

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
        }

        return workingDays;
    }

    private static int GetWorkingDaysInYear(int year)
    {
        int workingDays = 0;

        for (int month = 1; month <= 12; month++)
        {
            workingDays += TotalWorkDaysByMonth(year, month);
        }

        return workingDays;
    }
}

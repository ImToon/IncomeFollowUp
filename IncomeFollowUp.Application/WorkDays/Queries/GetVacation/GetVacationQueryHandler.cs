using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetVacation;

public class GetExtraDaysQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetExtraDaysQuery, int>
{
    public async Task<int> Handle(GetExtraDaysQuery request, CancellationToken cancellationToken)
    {
        var minDate = new DateTime(request.Year, 1, 1);
        var maxDate = new DateTime(request.Year + 1, 1, 1);
        var settings = await dbContext.Settings.FirstAsync(cancellationToken);
        var workDays = await dbContext.WorkDays.Where(w => w.Date >= minDate && w.Date < maxDate && w.IsWorkDay).ToListAsync(cancellationToken);

        var totalWorkingDays = GetWorkingDaysInYear(request.Year);

        var totalAvailableExtraDays = (totalWorkingDays * settings.DailyRate - (12 * settings.ExpectedMonthlyIncome)) / settings.DailyRate;

        var totalUsedExtraDays = workDays
            .GroupBy(wd => wd.Date.Month)
            .Sum(group =>
            {
                var totalExpected = TotalWorkDaysByMonth(request.Year, group.Key) * settings.DailyRate;
                var totalMonth = group.Sum(g => g.DailyRate);

                var total = totalExpected > settings.ExpectedMonthlyIncome ? totalExpected : settings.ExpectedMonthlyIncome;

                var outcomeDifference = total - totalMonth;

                return outcomeDifference != 0 ? outcomeDifference / settings.DailyRate : 0;
            });

        return totalAvailableExtraDays - totalUsedExtraDays;
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

using IncomeFollowUp.Application.WorkDays.Queries.Shared;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;

public class GetYearlyWorkDaysSummaryResponse
{
    public MonthlyWorkDaysSummary[] MonthlyWorkDaysSummaries { get; set; } = null!;
    public double AnnualExpenses { get; set; }
}
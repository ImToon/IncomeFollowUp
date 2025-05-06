using IncomeFollowUp.Application.WorkDays.Queries.Shared;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;

public class GetYearlyWorkDaysSummaryResponse
{
    public MonthlyWorkDaysSummary[] MonthlyWorkDaysSummaries { get; set; } = null!;
    public int TotalIncome { get; set; }
    public double TotalOutcome { get; set; }
    public int RemainingVacation { get; set; }
}
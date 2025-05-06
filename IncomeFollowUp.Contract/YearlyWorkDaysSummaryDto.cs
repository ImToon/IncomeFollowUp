namespace IncomeFollowUp.Contract;

public class YearlyWorkDaysSummaryDto
{
    public MonthlyWorkDaysSummaryDto[] MonthlyWorkDaysSummaries { get; set; } = null!;
    public int TotalIncome { get; set; }
    public double TotalOutcome { get; set; }
    public int RemainingVacation { get; set; }
}
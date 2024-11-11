namespace IncomeFollowUp.Contract;

public class YearlyWorkDaysSummaryDto
{
    public MonthlyWorkDaysSummaryDto[] MonthlyWorkDaysSummaries { get; set; } = null!;
    public double AnnualExpenses { get; set; }
}
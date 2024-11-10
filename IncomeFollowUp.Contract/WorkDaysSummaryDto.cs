namespace IncomeFollowUp.Contract;

public class WorkDaysSummaryDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int Total { get; set; }

    public DateTime Date => new(Year, Month, 1);
}
namespace IncomeFollowUp.Contract;

public class MonthlyIncomeDto
{
    public Guid Id { get; set; }
    public int ExpectedAmount { get; set; }
    public int ActualAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public List<WorkDayDto> WorkDays { get; set; } = [];
}

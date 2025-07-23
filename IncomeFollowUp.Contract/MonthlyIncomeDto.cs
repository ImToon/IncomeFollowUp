namespace IncomeFollowUp.Contract;

public class MonthlyIncomeDto
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Amount { get; set; }
}

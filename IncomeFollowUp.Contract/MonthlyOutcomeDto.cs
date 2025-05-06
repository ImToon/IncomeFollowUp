namespace IncomeFollowUp.Contract;

public class MonthlyOutcomeDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
}
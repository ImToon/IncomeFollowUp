namespace IncomeFollowUp.Domain;

public class MonthlyIncome
{
    public Guid Id { get; set; }
    public int ExpectedAmount { get; set; }
    public int ActualAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public virtual List<WorkDay> WorkDays { get; set; } = default!;

    public void Update(int actualAmount)
    {
        ActualAmount = actualAmount;
    }
}
namespace IncomeFollowUp.Domain;

public class MonthlyOutcome
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }

    public void Update(string name, double amount)
    {
        Name = name;
        Amount = amount;
    }
}
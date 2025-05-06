namespace IncomeFollowUp.Domain;

public class Settings
{
    public Guid Id { get; set; }
    public int DailyRate { get; set; }
    public int ExpectedMonthlyIncome { get; set; }

    public void Update(int dailyRate, int expectedMonthlyIncome)
    {
        DailyRate = dailyRate;
        ExpectedMonthlyIncome = expectedMonthlyIncome;
    }
}
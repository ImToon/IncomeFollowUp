namespace IncomeFollowUp.Domain;

public class WorkDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsWorkDay { get; set; }
    public int DailyRate { get; set; }
    public Guid MonthlyIncomeId { get; set; }
    public virtual MonthlyIncome MonthlyIncome { get; set; } = default!;

    public void Update(bool isWorkDay)
    {
        IsWorkDay = isWorkDay;
    }
}
namespace IncomeFollowUp.Domain;

public class WorkDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsWorkDay { get; set; }
    public int DailyRate { get; set; }

    public void Update(bool isWorkDay)
    {
        IsWorkDay = isWorkDay;
    }
}
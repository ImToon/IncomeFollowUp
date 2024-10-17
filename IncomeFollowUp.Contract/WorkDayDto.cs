namespace IncomeFollowUp.Contract;

public class WorkDayDto
{
    public Guid? Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsWorkDay { get; set; }
    public int DailyRate { get; set; }
}
namespace IncomeFollowUp.Contract;

public class SyncDataDto
{
    public List<SettingsDto>? Settings { get; set; }
    public List<MonthlyOutcomeDto>? MonthlyOutcomes { get; set; }
    public List<MonthlyIncomeDto>? MonthlyIncomes { get; set; }
}

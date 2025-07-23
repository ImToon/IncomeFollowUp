namespace IncomeFollowUp.Contract;

public class ExportDataDto
{
    public List<SettingsDto>? Settings { get; set; }
    public List<MonthlyOutcomeDto>? MonthlyOutcomes { get; set; }
    public List<WorkDayDto>? WorkDays { get; set; }
    public List<MonthlyIncomeDto>? MonthlyIncomes { get; set; }
}

using MediatR;

namespace IncomeFollowUp.Application.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommand: IRequest<Domain.Settings>
{
    public int DailyRate { get; set; }
    public int ExpectedMonthlyIncome { get; set; }
}
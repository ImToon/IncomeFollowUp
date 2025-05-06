using MediatR;

namespace IncomeFollowUp.Application.Settings.Commands.CreateSettings;

public class CreateSettingsCommand: IRequest<Domain.Settings>
{
    public int DailyRate { get; set; }
    public int ExpectedMonthlyIncome { get; set; }
}
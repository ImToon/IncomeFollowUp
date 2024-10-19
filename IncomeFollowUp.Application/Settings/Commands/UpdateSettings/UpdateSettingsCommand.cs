using MediatR;

namespace IncomeFollowUp.Application.Settings.Commands.UpdateSettings;

public record UpdateSettingsCommand(int DailyRate, int ExpectedMonthlyIncome): IRequest<Domain.Settings>;

using MediatR;

namespace IncomeFollowUp.Application.Settings.Commands.CreateSettings;

public record CreateSettingsCommand (int DailyRate, int ExpectedMonthlyIncome): IRequest<Domain.Settings>;

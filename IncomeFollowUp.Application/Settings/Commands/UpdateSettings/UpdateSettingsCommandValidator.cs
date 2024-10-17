using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;
using IncomeFollowUp.Infrastructure;

namespace IncomeFollowUp.Application.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator()
    {
        RuleFor(x => x.DailyRate)
            .GreaterThanOrEqualToWithMessage(0);
     
        RuleFor(x => x.ExpectedMonthlyIncome)
            .GreaterThanOrEqualToWithMessage(0);
    }
}
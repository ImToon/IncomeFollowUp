using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Settings.Commands.CreateSettings;

public class CreateSettingsCommandValidator : AbstractValidator<CreateSettingsCommand>
{
    public CreateSettingsCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x.DailyRate)
            .GreaterThanOrEqualToWithMessage(0);

        RuleFor(x => x.ExpectedMonthlyIncome)
            .GreaterThanOrEqualToWithMessage(0);

        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var settings = await dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
                return settings == null;
            })
            .WithMessage("Settings are already created.");
    }
}

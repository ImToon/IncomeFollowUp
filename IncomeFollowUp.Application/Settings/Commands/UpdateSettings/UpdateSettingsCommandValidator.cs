using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var settings = await dbContext.Settings.FirstAsync(cancellationToken);
                return settings != null;
            })
            .WithMessage("Settings must exist.");

        RuleFor(x => x.DailyRate)
            .GreaterThanOrEqualToWithMessage(0);
     
        RuleFor(x => x.ExpectedMonthlyIncome)
            .GreaterThanOrEqualToWithMessage(0);
    }
}
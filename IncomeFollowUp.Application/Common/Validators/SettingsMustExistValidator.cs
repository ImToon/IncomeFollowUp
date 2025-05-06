using FluentValidation;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Common.Validators;

public class SettingsMustExistValidator<T> : AbstractValidator<T>
{
    public SettingsMustExistValidator(IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) =>
            {
                var settings = await dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
                return settings != null;
            })
            .WithMessage("You must setup settings first.");
    }
}
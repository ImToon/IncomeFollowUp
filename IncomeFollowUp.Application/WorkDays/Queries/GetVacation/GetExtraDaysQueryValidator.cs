using FluentValidation;
using IncomeFollowUp.Application.Common.Validators;
using IncomeFollowUp.Infrastructure;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetVacation;

public class GetExtraDaysQueryValidator : AbstractValidator<GetExtraDaysQuery>
{
    public GetExtraDaysQueryValidator (IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .SetValidator(new SettingsMustExistValidator<GetExtraDaysQuery>(dbContext));
    }
}
using FluentValidation;
using IncomeFollowUp.Application.Common.Validators;
using IncomeFollowUp.Infrastructure;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetVacation;

public class GetVacationQueryValidator : AbstractValidator<GetVacationQuery>
{
    public GetVacationQueryValidator (IncomeFollowUpContext dbContext)
    {
        RuleFor(x => x)
            .SetValidator(new SettingsMustExistValidator<GetVacationQuery>(dbContext));
    }
}
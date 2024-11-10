using FluentValidation;
using IncomeFollowUp.Application.Common.Validators;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;

public class GetWorkDaysQueryValidator : AbstractValidator<GetWorkDaysQuery>
{
    public GetWorkDaysQueryValidator()
    {
        RuleFor(x => x.Month)
            .SetValidator(new MonthValidator());

    }
}
using FluentValidation;
using IncomeFollowUp.Application.Common.Extensions;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetMonthlyWorkDaysSummaries;


public class GetMonthlyWorkDaysSummariesQueryValidator : AbstractValidator<GetMonthlyWorkDaysSummariesQuery>
{
    public GetMonthlyWorkDaysSummariesQueryValidator()
    {
        RuleFor(x => x.NumberOfLatestMonths)
            .GreaterThanOrEqualToWithMessage(1, "Number of latest months must be greater than or equal to 1.");
    }
}
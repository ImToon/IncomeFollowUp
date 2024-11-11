using FluentValidation;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;


public class GetYearlyWorkDaysSummaryQueryValidator : AbstractValidator<GetYearlyWorkDaysSummaryQuery>
{
    public GetYearlyWorkDaysSummaryQueryValidator()
    {
        RuleFor(x => x.Year)
            .Must(x => x > 0)
            .WithMessage("Year must be greater than 0.");
    }
}
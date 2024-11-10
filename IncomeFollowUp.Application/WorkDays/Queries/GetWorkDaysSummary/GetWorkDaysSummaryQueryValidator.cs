using FluentValidation;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;


public class GetWorkDaysSummaryQueryValidator : AbstractValidator<GetWorkDaysSummaryQuery>
{
    public GetWorkDaysSummaryQueryValidator()
    {
        RuleFor(x => x.Year)
            .NotNull()
            .When(x => x.SummaryType == SummaryType.Yearly)
            .DependentRules(() =>
            {
                RuleFor(x => x.Year)
                    .Must(x => x > 0)
                    .When(x => x.SummaryType == SummaryType.Yearly)
                    .WithMessage("Year must be greater than 0.");
            })
            .WithMessage("Year is required for yearly summary.");
   
        
    }
}
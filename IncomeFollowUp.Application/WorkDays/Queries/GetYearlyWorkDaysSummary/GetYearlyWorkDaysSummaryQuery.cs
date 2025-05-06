using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;

public class GetYearlyWorkDaysSummaryQuery : IRequest<GetYearlyWorkDaysSummaryResponse>
{
    public int Year { get; set; }
}
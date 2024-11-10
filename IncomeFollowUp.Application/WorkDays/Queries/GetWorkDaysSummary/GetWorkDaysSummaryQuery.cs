using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;

public enum SummaryType
{
    Monthly = 0,
    Yearly
}

public class GetWorkDaysSummaryQuery : IRequest<IEnumerable<GetWorkDaysSummaryResponse>>
{
    public SummaryType SummaryType { get; set; }
    public int? Year { get; set; }
}
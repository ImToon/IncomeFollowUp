using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;

public class GetWorkDaysSummaryQuery : IRequest<IEnumerable<GetWorkDaysSummaryResponse>>
{
}
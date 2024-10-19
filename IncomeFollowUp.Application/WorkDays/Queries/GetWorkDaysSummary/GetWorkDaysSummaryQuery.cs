using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;

public record GetWorkDaysSummaryQuery : IRequest<IEnumerable<GetWorkDaysSummaryResponse>>;

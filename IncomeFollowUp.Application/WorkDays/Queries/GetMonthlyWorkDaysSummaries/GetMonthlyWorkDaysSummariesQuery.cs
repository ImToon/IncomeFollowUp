using IncomeFollowUp.Application.WorkDays.Queries.Shared;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetMonthlyWorkDaysSummaries;

public class GetMonthlyWorkDaysSummariesQuery(int? numberOfLatestMonths) : IRequest<MonthlyWorkDaysSummary[]>
{
    public int NumberOfLatestMonths { get; set; } = numberOfLatestMonths ?? 6;
}
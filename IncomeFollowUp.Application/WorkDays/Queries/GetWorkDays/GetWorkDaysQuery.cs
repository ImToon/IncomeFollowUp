using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;

public class GetWorkDaysQuery : IRequest<IEnumerable<WorkDay>>
{
    public int Month { get; set; }
    public int Year { get; set; }
}
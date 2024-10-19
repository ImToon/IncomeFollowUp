using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;

public record GetWorkDaysQuery(int Month, int Year) : IRequest<IEnumerable<WorkDay>>;

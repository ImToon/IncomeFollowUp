using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Queries.GetMonthlyOutcomes;

public class GetMonthlyOutcomesQuery : IRequest<MonthlyOutcome[]>
{

}
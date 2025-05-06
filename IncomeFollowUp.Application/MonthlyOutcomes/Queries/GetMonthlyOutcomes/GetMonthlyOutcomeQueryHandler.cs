using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Queries.GetMonthlyOutcomes;

public class GetMonthlyOutcomeQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetMonthlyOutcomesQuery, MonthlyOutcome[]>
{
    public async Task<MonthlyOutcome[]> Handle(GetMonthlyOutcomesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.MonthlyOutcomes.ToArrayAsync(cancellationToken);
    }
}
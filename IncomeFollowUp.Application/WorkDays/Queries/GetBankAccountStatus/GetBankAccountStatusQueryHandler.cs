using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetBankAccountStatus;

public class GetBankAccountStatusQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetBankAccountStatusQuery, double>
{
    public async Task<double> Handle(GetBankAccountStatusQuery request, CancellationToken cancellationToken)
    {
        var incomes = await dbContext.MonthlyIncomes.ToListAsync(cancellationToken);
        var outcomes = await dbContext.MonthlyOutcomes.ToListAsync(cancellationToken);

        var totalIncome = incomes.Sum(x => x.ActualAmount);
        var totalOutcome = outcomes.Sum(x => x.Amount) * incomes.Count;

        return totalIncome - totalOutcome;
    }
}
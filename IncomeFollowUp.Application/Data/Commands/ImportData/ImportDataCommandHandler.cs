using IncomeFollowUp.Infrastructure;
using MediatR;

namespace IncomeFollowUp.Application.Data.Commands.ImportData;

public class ImportDataCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<ImportDataCommand, Unit>
{
    private readonly IncomeFollowUpContext _dbContext = dbContext;

    public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
    {
        if (request.Settings != null && request.Settings.Count != 0)
        {
            await _dbContext.Settings.AddRangeAsync(request.Settings, cancellationToken);
        }

        if (request.MonthlyOutcomes != null && request.MonthlyOutcomes.Count != 0)
        {
            await _dbContext.MonthlyOutcomes.AddRangeAsync(request.MonthlyOutcomes, cancellationToken);
        }

        if (request.MonthlyIncomes != null && request.MonthlyIncomes.Count != 0)
        {
            foreach (var income in request.MonthlyIncomes)
            {
                income.WorkDays = [.. request.WorkDays?.Where(wd => wd.MonthlyIncomeId == income.Id) ?? []];
            }
            
            await _dbContext.MonthlyIncomes.AddRangeAsync(request.MonthlyIncomes, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

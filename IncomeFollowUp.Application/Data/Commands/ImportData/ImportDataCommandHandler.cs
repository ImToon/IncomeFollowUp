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
            _dbContext.Settings.RemoveRange(_dbContext.Settings);
            await _dbContext.Settings.AddRangeAsync(request.Settings, cancellationToken);
        }

        if (request.MonthlyOutcomes != null && request.MonthlyOutcomes.Count != 0)
        {
            _dbContext.MonthlyOutcomes.RemoveRange(_dbContext.MonthlyOutcomes);
            await _dbContext.MonthlyOutcomes.AddRangeAsync(request.MonthlyOutcomes, cancellationToken);
        }

        if (request.WorkDays != null && request.WorkDays.Count != 0)
        {
            _dbContext.WorkDays.RemoveRange(_dbContext.WorkDays);
            await _dbContext.WorkDays.AddRangeAsync(request.WorkDays, cancellationToken);
        }

        if (request.MonthlyIncomes != null && request.MonthlyIncomes.Count != 0)
        {
            _dbContext.MonthlyIncomes.RemoveRange(_dbContext.MonthlyIncomes);
            await _dbContext.MonthlyIncomes.AddRangeAsync(request.MonthlyIncomes, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

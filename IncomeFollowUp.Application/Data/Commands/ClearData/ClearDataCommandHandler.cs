using MediatR;
using IncomeFollowUp.Infrastructure;

namespace IncomeFollowUp.Application.Data.Commands.ClearData;

public class ClearDataCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<ClearDataCommand, Unit>
{
    private readonly IncomeFollowUpContext _dbContext = dbContext;

    public async Task<Unit> Handle(ClearDataCommand request, CancellationToken cancellationToken)
    {
        _dbContext.WorkDays.RemoveRange(_dbContext.WorkDays);
        _dbContext.MonthlyOutcomes.RemoveRange(_dbContext.MonthlyOutcomes);
        _dbContext.MonthlyIncomes.RemoveRange(_dbContext.MonthlyIncomes);
        _dbContext.Settings.RemoveRange(_dbContext.Settings);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
using IncomeFollowUp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Data.Commands.ExportData;

public class ExportDataCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<ExportDataCommand, ExportDataResult>
{
    private readonly IncomeFollowUpContext _dbContext = dbContext;

    public async Task<ExportDataResult> Handle(ExportDataCommand request, CancellationToken cancellationToken)
    {
        return new ExportDataResult
        {
            Settings = await _dbContext.Settings.ToListAsync(cancellationToken),
            MonthlyOutcomes = await _dbContext.MonthlyOutcomes.ToListAsync(cancellationToken),
            WorkDays = await _dbContext.WorkDays.ToListAsync(cancellationToken),
            MonthlyIncomes = await _dbContext.MonthlyIncomes.ToListAsync(cancellationToken)
        };
    }
}

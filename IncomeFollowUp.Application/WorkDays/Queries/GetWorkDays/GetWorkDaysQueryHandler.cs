using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;

public class GetWorkDaysQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetWorkDaysQuery, IEnumerable<WorkDay>>
{
    public async Task<IEnumerable<WorkDay>> Handle(GetWorkDaysQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.WorkDays
            .Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year)
            .ToListAsync(cancellationToken);
    }
}
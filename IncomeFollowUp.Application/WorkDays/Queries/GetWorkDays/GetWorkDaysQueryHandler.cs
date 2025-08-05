using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;
using System.Security.Cryptography.X509Certificates;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;

public class GetWorkDaysQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetWorkDaysQuery, IEnumerable<WorkDay>>
{
    public async Task<IEnumerable<WorkDay>> Handle(GetWorkDaysQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.WorkDays
            .Where(x => x.Date.Year == request.Year && x.Date.Month == request.Month)
            .OrderBy(x => x.Date )
            .ToArrayAsync(cancellationToken);
    }
}
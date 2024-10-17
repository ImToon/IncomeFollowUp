using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Settings.Queries.GetSettings;

public class GetSettingsQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GetSettingsQuery, Domain.Settings?>
{
    public async Task<Domain.Settings?> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Settings.FirstOrDefaultAsync(cancellationToken);
    }
}
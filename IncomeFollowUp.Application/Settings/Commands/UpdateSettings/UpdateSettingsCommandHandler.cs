using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Application.Settings.Commands.UpdateSettings;

public class GetSettingsQueryHandler(IncomeFollowUpContext dbContext) : IRequestHandler<UpdateSettingsCommand, Domain.Settings>
{
    public async Task<Domain.Settings> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        var settingsToUpdate = await dbContext.Settings.FirstAsync(cancellationToken);

        settingsToUpdate.Update(request.DailyRate, request.ExpectedMonthlyIncome);

        await dbContext.SaveChangesAsync(cancellationToken);

        return settingsToUpdate;
    }
}

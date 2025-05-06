using MediatR;
using IncomeFollowUp.Infrastructure;
using MapsterMapper;

namespace IncomeFollowUp.Application.Settings.Commands.CreateSettings;

public class CreateSettingsCommandHandler(IncomeFollowUpContext dbContext, IMapper mapper) : IRequestHandler<CreateSettingsCommand, Domain.Settings>
{
    public async Task<Domain.Settings> Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
    {
        var createdSettings = mapper.Map<Domain.Settings>(request);
        dbContext.Settings.Add(createdSettings);

        await dbContext.SaveChangesAsync(cancellationToken);
        return createdSettings;
    }
}
using MediatR;
using IncomeFollowUp.Infrastructure;
using MapsterMapper;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.CreateMonthlyOutcome;

public class CreateMonthlyOutcomeCommandHandler(IncomeFollowUpContext dbContext, IMapper mapper) : IRequestHandler<CreateMonthlyOutcomeCommand, MonthlyOutcome>
{
    public async Task<MonthlyOutcome> Handle(CreateMonthlyOutcomeCommand request, CancellationToken cancellationToken)
    {
        var createdMonthlyOutcome = mapper.Map<MonthlyOutcome>(request);
        dbContext.MonthlyOutcomes.Add(createdMonthlyOutcome);

        await dbContext.SaveChangesAsync(cancellationToken);
        return createdMonthlyOutcome;
    }
}
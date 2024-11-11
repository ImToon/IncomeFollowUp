using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.UpdateMonthlyOutcome;

public class UpdateMonthlyOutcomeCommand: IRequest<MonthlyOutcome>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
}
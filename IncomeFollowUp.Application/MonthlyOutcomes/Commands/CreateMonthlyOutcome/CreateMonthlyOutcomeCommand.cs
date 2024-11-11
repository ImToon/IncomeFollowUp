using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.CreateMonthlyOutcome;

public class CreateMonthlyOutcomeCommand : IRequest<MonthlyOutcome>
{
    public string Name { get; set; }
    public double Amount { get; set; }
}
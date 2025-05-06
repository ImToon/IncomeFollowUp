using MediatR;

namespace IncomeFollowUp.Application.MonthlyOutcomes.Commands.DeleteMonthlyOutcome;

public class DeleteMonthlyOutcomeCommand: IRequest
{
    public Guid Id { get; set; }
}
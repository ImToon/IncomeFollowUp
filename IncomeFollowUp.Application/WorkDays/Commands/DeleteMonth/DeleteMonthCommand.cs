using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Commands.DeleteMonth;

public class DeleteMonthCommand : IRequest
{
    public int Month { get; set; }
    public int Year { get; set; }
}
using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;

public class GenerateMonthCommand: IRequest<IEnumerable<WorkDay>>
{
    public int Month { get; set; }
    public int Year { get; set; }
}
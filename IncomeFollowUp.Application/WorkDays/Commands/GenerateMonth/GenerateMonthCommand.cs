using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;

public record GenerateMonthCommand(int Month, int Year): IRequest<IEnumerable<WorkDay>>;

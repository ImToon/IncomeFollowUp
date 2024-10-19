using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;

public record UpdateWorkDayCommand(Guid Id, bool IsWorkDay);
public record UpdateWorkDaysCommand(IEnumerable<UpdateWorkDayCommand> UpdateWorkDayCommands = null!) : IRequest<IEnumerable<WorkDay>>;

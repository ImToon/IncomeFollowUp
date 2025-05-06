using IncomeFollowUp.Domain;
using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;

public class UpdateWorkDayCommand
{
    public Guid Id { get; set; }
    public bool IsWorkDay { get; set; }
}

public class UpdateWorkDaysCommand : IRequest<IEnumerable<WorkDay>>
{
    public IEnumerable<UpdateWorkDayCommand> UpdateWorkDayCommands { get; set; } = null!;    
}
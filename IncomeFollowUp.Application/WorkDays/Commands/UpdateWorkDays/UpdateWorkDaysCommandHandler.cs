using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;

public class UpdateWorkDaysCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<UpdateWorkDaysCommand, IEnumerable<WorkDay>>
{
    public async Task<IEnumerable<WorkDay>> Handle(UpdateWorkDaysCommand request, CancellationToken cancellationToken)
    {
        
        List<WorkDay> workDays = await dbContext.WorkDays.Where(wd => request.UpdateWorkDayCommands.Select(c => c.Id).ToList().Contains(wd.Id)).ToListAsync(cancellationToken);

        foreach(var command in request.UpdateWorkDayCommands)
        {
            var workDay = workDays.First(wd => wd.Id == command.Id);
            workDay.Update(command.IsWorkDay);
        }

        dbContext.WorkDays.UpdateRange(workDays);
        await dbContext.SaveChangesAsync(cancellationToken);

        return workDays;
    }
}
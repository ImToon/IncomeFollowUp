using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;

public class UpdateWorkDaysCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<UpdateWorkDaysCommand, IEnumerable<WorkDay>>
{
    public async Task<IEnumerable<WorkDay>> Handle(UpdateWorkDaysCommand request, CancellationToken cancellationToken)
    {
        
        List<WorkDay> workDays = await dbContext.WorkDays
                                        .Where(wd => request.UpdateWorkDayCommands.Select(c => c.Id).ToList().Contains(wd.Id))
                                        .Include(wd => wd.MonthlyIncome)
                                        .ToListAsync(cancellationToken);
        MonthlyIncome monthlyIncome = workDays.First().MonthlyIncome;
        int currentMonthlyIncome = monthlyIncome.ActualAmount;

        foreach(var command in request.UpdateWorkDayCommands)
        {
            var workDay = workDays.First(wd => wd.Id == command.Id);
            currentMonthlyIncome += command.IsWorkDay ? workDay.DailyRate : -workDay.DailyRate;
            workDay.Update(command.IsWorkDay);
        }

        monthlyIncome.Update(currentMonthlyIncome);

        dbContext.WorkDays.UpdateRange(workDays);
        dbContext.MonthlyIncomes.Update(monthlyIncome);
        await dbContext.SaveChangesAsync(cancellationToken);

        return workDays;
    }
}
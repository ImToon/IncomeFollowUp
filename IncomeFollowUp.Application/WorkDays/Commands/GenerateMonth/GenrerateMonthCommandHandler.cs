using MediatR;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using IncomeFollowUp.Domain;
using IncomeFollowUp.Application.Common.Utils;

namespace IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;

public class GenrerateMonthCommandHandler(IncomeFollowUpContext dbContext) : IRequestHandler<GenerateMonthCommand, IEnumerable<WorkDay>>
{
    public async Task<IEnumerable<WorkDay>> Handle(GenerateMonthCommand request, CancellationToken cancellationToken)
    {
        var currentSettings = await dbContext.Settings.FirstAsync(cancellationToken);
        var workDays = DateUtils.GetWeekdaysOfMonth(request.Year, request.Month).Select(date => new WorkDay { Date = date, IsWorkDay = true, DailyRate = currentSettings.DailyRate }).ToList();

        dbContext.WorkDays.AddRange(workDays);
        await dbContext.SaveChangesAsync(cancellationToken);

        return workDays;
    }
}
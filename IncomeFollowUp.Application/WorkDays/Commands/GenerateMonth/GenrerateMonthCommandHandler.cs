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

        int monthAmount = workDays.Count * currentSettings.DailyRate;
        var monthlyIncome = new MonthlyIncome
        {
            Year = request.Year,
            Month = request.Month,
            ExpectedAmount = monthAmount,
            ActualAmount = monthAmount,
            WorkDays = workDays
        };

        dbContext.MonthlyIncomes.Add(monthlyIncome);
        await dbContext.SaveChangesAsync(cancellationToken);

        return workDays;
    }
}
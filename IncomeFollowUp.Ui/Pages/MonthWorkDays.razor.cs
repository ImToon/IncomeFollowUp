using System.Globalization;
using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;

namespace IncomeFollowUp.Ui.Pages;

public partial class MonthWorkDays
{
    [Inject]
    public WorkDaysService WorkDaysService { get; set; } = null!;

    [SupplyParameterFromQuery]
    public int Year { get; set; }

    [SupplyParameterFromQuery]
    public int Month { get; set; }
    
    public List<List<WorkDayDto>> WorkDaysByWeek { get; set; } = [];

    public List<UpdateWorkDayDto> WorkDaysUpdates { get; set; } = [];
    public bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var workDaysDtos = await WorkDaysService.GetWorkDays(Year, Month);

        WorkDaysByWeek = workDaysDtos.GroupBy(d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
            .OrderBy(g => g.Key)
            .Select(g => g.ToList())
            .ToList();
    }

    private int TotalMonth => WorkDaysByWeek.SelectMany(w => w).Where(d => d.IsWorkDay).Sum(d => d.DailyRate);
    private int TotalWorkDays => WorkDaysByWeek.SelectMany(w => w).Where(d => d.IsWorkDay).Count();

    private void SwitchWorkDay(WorkDayDto workDay)
    {
        foreach(var week in WorkDaysByWeek)
        {
            foreach(var day in week)
            {
                if (day.Date == workDay.Date)
                {
                    day.IsWorkDay = !day.IsWorkDay;

                    if (WorkDaysUpdates.Any(u => u.Id == day.Id))
                    {
                        WorkDaysUpdates.Remove(WorkDaysUpdates.First(u => u.Id == day.Id));
                    }
                    else
                    {
                        WorkDaysUpdates.Add(new UpdateWorkDayDto
                        {
                            Id = day.Id!.Value,
                            IsWorkDay = day.IsWorkDay,
                        });
                    }
                }
            }
        }

        StateHasChanged();
    }

    private void ResetChanges()
    {
        foreach(var week in WorkDaysByWeek)
        {
            foreach(var day in week)
            {
                if (WorkDaysUpdates.Any(u => u.Id == day.Id))
                {
                    day.IsWorkDay = !day.IsWorkDay;
                }
            }
        }

        WorkDaysUpdates.Clear();

        StateHasChanged();
    }

    private async Task SaveChanges()
    {
        await WorkDaysService.UpdateWorkDay(WorkDaysUpdates);
        WorkDaysUpdates.Clear();
    }
}
using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;

namespace IncomeFollowUp.Ui.Pages;
public partial class YearSummary
{
    [Inject]
    public required WorkDaysService WorkDaysService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public bool IsLoading { get; set; }
    public int Year { get; set; } = DateTime.Now.Year;
    private IEnumerable<WorkDaysSummaryDto> WorkDaysSummaryDtos { get; set; } = [];
    private readonly string[] headings = ["Month", "Total"];

    protected override async Task OnInitializedAsync()
    {
        await Fetch();
    }

    private async Task PreviousYear()
    {
        Year--;
        await Fetch();
    }

    private async Task NextYear()
    {
        Year++;
        await Fetch();
    }

    private async Task Fetch()
    {
        IsLoading = true;
        WorkDaysSummaryDtos = await WorkDaysService.GetSummary(SummaryType.Yearly, Year);
        IsLoading = false;
    }

    private void NavigateToWorkDays(WorkDaysSummaryDto workDaysSummaryDto)
    {
        NavigationManager.NavigateTo($"workdays?year={workDaysSummaryDto.Year}&month={workDaysSummaryDto.Month}");
    }
}
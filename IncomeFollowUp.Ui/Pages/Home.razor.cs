using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Components;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Pages;

public partial class Home
{
    [Inject]
    public required WorkDaysService WorkDaysService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    private readonly string[] headings = ["Month", "Total"];

    private IEnumerable<WorkDaysSummaryDto> WorkDaysSummaryDtos { get; set; } = [];
    public int ExtraDays { get; set; }

    protected override async Task OnInitializedAsync()
    {
        WorkDaysSummaryDtos = await WorkDaysService.GetSummary(SummaryType.Monthly);
        ExtraDays = await WorkDaysService.GetExtraDays();        
    }

    private Task<IDialogReference> OpenDialogAsync()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        return DialogService.ShowAsync<CreateMonthDialog>("Simple Dialog", options);
    }

    private void NavigateToWorkDays(WorkDaysSummaryDto workDaysSummaryDto)
    {
        NavigationManager.NavigateTo($"workdays?year={workDaysSummaryDto.Year}&month={workDaysSummaryDto.Month}");
    }
}
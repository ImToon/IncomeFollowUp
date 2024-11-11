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

    public bool IsLoading { get; set; }
    public int Year { get; set; } = DateTime.Now.Year;
    private YearlyWorkDaysSummaryDto YearlyWorkDaysSummaryDto { get; set; } = new();
    public int ExtraDays { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await FetchYearlySummary(); 
        IsLoading = false;
    }

     private async Task PreviousYear()
    {
        Year--;
        await FetchYearlySummary();
    }

    private async Task NextYear()
    {
        Year++;
        await FetchYearlySummary();
    }

    private async Task FetchYearlySummary()
    {
        IsLoading = true;
        ExtraDays = await WorkDaysService.GetExtraDays(Year);       
        YearlyWorkDaysSummaryDto = await WorkDaysService.GetYearlySummary(Year);
        IsLoading = false;
    }

     private async void GenerateNexMonth()
    {
        var nextMonth = YearlyWorkDaysSummaryDto.MonthlyWorkDaysSummaries.OrderByDescending(mwd => mwd.Date).FirstOrDefault()?.Date.AddMonths(1) ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        await WorkDaysService.GenerateMonth(nextMonth.Year, nextMonth.Month);
        NavigationManager.NavigateTo($"workdays?year={nextMonth.Year}&month={nextMonth.Month}");
    }

    private async Task OpenDeleteDialog(MonthlyWorkDaysSummaryDto workDaysSummaryDto)
    {
        var parameters = new DialogParameters<DeleteDialog>
        {
            { x => x.DeleteCallback, new EventCallbackFactory().Create(this, () => WorkDaysService.DeleteMonth(workDaysSummaryDto.Date.Year, workDaysSummaryDto.Date.Month)) },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

        var dialog = await DialogService.ShowAsync<DeleteDialog>("Delete monthly outcome", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            var nbMonths = YearlyWorkDaysSummaryDto.MonthlyWorkDaysSummaries.Length;
            YearlyWorkDaysSummaryDto.AnnualExpenses = YearlyWorkDaysSummaryDto.AnnualExpenses / nbMonths * (nbMonths - 1);
            YearlyWorkDaysSummaryDto.MonthlyWorkDaysSummaries = YearlyWorkDaysSummaryDto.MonthlyWorkDaysSummaries.Where(x => x.Date != workDaysSummaryDto.Date).ToArray();
        }
    }

    private Task<IDialogReference> OpenMonthCreationDialog()
    {
        var parameters = new DialogParameters<CreateMonthDialog>
        {
            { x => x.Year, Year },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

        return DialogService.ShowAsync<CreateMonthDialog>("Create month", parameters, options);
    }

    private void NavigateToWorkDays(TableRowClickEventArgs<MonthlyWorkDaysSummaryDto> eventArgs)
    {
        var workDaysSummaryDto = eventArgs.Item!;
        NavigationManager.NavigateTo($"workdays?year={workDaysSummaryDto.Year}&month={workDaysSummaryDto.Month}");
    }
}
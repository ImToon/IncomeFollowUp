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
    public double BankAccountStatus { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await LoadData(); 
        IsLoading = false;
    }

    private async Task PreviousYear()
    {
        Year--;
        await LoadData();
    }

    private async Task NextYear()
    {
        Year++;
        await LoadData();
    }

    private async Task LoadData()
    {
        IsLoading = true;
        YearlyWorkDaysSummaryDto = await WorkDaysService.GetYearlySummary(Year);
        BankAccountStatus = await WorkDaysService.GetBankAccountStatus();
        IsLoading = false;
    }

    private Task<IDialogReference> OpenMonthCreationDialog()
    {
        var date = YearlyWorkDaysSummaryDto.MonthlyWorkDaysSummaries.OrderByDescending(mwd => mwd.Date).FirstOrDefault()?.Date.AddMonths(1) ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        var parameters = new DialogParameters<CreateMonthDialog>
        {
            { x => x.Date, date },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

        return DialogService.ShowAsync<CreateMonthDialog>("Create month", parameters, options);
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
            await LoadData();

        }
    }

    private void NavigateToWorkDays(TableRowClickEventArgs<MonthlyWorkDaysSummaryDto> eventArgs)
    {
        var workDaysSummaryDto = eventArgs.Item!;
        NavigationManager.NavigateTo($"workdays?year={workDaysSummaryDto.Year}&month={workDaysSummaryDto.Month}");
    }
}
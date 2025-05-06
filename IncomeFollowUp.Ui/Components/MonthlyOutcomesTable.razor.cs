using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Components;

public partial class MonthlyOutcomesTable
{
    [Inject]
    public required IDialogService DialogService { get; set; }

    [Inject]
    public required MonthlyOutcomeService MonthlyOutcomeService { get; set; }

    [Parameter]
    public MonthlyOutcomeDto[] MonthlyOutcomes { get; set; } = [];
    private async Task OpenMonthlyOutcomeCreationDialog()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<CreateMonthlyOutcomeDialog>("Create monthly outcome", options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            if (result.Data is MonthlyOutcomeDto monthlyOutcome)
            {
                MonthlyOutcomes = [.. MonthlyOutcomes, monthlyOutcome];
            }
        }
    }

    private async Task OpenDeleteDialog(MonthlyOutcomeDto monthlyOutcome)
    {
        var parameters = new DialogParameters<DeleteDialog>
        {
            { x => x.DeleteCallback, new EventCallbackFactory().Create(this, () => MonthlyOutcomeService.DeleteMonthlyOutcome(monthlyOutcome.Id!.Value)) },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

        var dialog = await DialogService.ShowAsync<DeleteDialog>("Delete monthly outcome", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            MonthlyOutcomes = MonthlyOutcomes.Where(x => x.Id != monthlyOutcome.Id).ToArray();
        }
    }

    private async Task OpenUpdateDialog(MonthlyOutcomeDto monthlyOutcome)
    {
        var parameters = new DialogParameters<UpdateMonthlyOutcomeDialog>
        {
            { x => x.MonthlyOutcomeDto, monthlyOutcome },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

        var dialog = await DialogService.ShowAsync<UpdateMonthlyOutcomeDialog>("Update monthly outcome", parameters, options);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            MonthlyOutcomes = MonthlyOutcomes.Where(x => x.Id != monthlyOutcome.Id).ToArray();
            MonthlyOutcomes = [.. MonthlyOutcomes, monthlyOutcome];
        }
    }
}
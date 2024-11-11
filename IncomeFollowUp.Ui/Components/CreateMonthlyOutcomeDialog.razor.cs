using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Components;

public partial class CreateMonthlyOutcomeDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Inject]
    public required MonthlyOutcomeService MonthlyOutcomeService { get; set; }

    public bool IsLoading { get; set; }
    public MonthlyOutcomeDto MonthlyOutcomeDto { get; set; } = new();

    private async Task Create()
    {
        IsLoading = true;
        var monthlyOutcome = await MonthlyOutcomeService.CreateMonthlyOutcome(MonthlyOutcomeDto);
        IsLoading = false;
        MudDialog.Close(DialogResult.Ok(monthlyOutcome));
    }

    private void Cancel() => MudDialog.Cancel();
}
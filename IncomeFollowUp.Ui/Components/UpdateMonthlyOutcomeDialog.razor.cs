using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Components;

public partial class UpdateMonthlyOutcomeDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Inject]
    public required MonthlyOutcomeService MonthlyOutcomeService { get; set; }

    [Parameter]
    public required MonthlyOutcomeDto MonthlyOutcomeDto { get; set; }

    public bool IsLoading { get; set; }

    private async Task Update()
    {
        IsLoading = true;
        await MonthlyOutcomeService.UpdateMonthlyOutcome(MonthlyOutcomeDto);
        IsLoading = false;
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Components;

public partial class CreateMonthDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Inject]
    public required WorkDaysService WorkDaysService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public bool IsLoading { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    private async Task Submit()
    {
        IsLoading = true;
        await WorkDaysService.GenerateMonth(Month, Year);
        IsLoading = false;
        NavigationManager.NavigateTo($"workdays?year={Year}&month={Month}");
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
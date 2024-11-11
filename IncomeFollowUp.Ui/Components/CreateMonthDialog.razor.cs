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

    [Parameter]
    public int Year { get; set; } = DateTime.Now.Year;
    public int Month { get; set; } = 1;
    public bool IsLoading { get; set; }

    private async Task Submit()
    {
        IsLoading = true;
        await WorkDaysService.GenerateMonth(Year, Month);
        IsLoading = false;
        NavigationManager.NavigateTo($"workdays?year={Year}&month={Month}");
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
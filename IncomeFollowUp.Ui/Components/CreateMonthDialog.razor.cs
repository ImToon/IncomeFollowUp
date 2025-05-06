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
    public DateTime Date { get; set; }
    public bool IsLoading { get; set; }
    private DateTime? _date;

    protected override void OnInitialized()
    {
        _date = Date;
        base.OnInitialized();
    }

    private async Task Submit()
    {
        Date = _date!.Value;
        IsLoading = true;
        await WorkDaysService.GenerateMonth(Date.Year, Date.Month);
        IsLoading = false;
        NavigationManager.NavigateTo($"workdays?year={Date.Year}&month={Date.Month}");
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IncomeFollowUp.Ui.Components;

public partial class DeleteDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public EventCallback DeleteCallback { get; set; }

    public bool IsLoading { get; set; }

    private async Task Delete()
    {
        IsLoading = true;
        await DeleteCallback.InvokeAsync();
        IsLoading = false;
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
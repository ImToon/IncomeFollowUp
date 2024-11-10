using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace IncomeFollowUp.Ui.Pages;

public partial class Settings
{
    [Inject]
    public required SettingsService SettingsService { get; set; }

    public bool IsLoading { get; set; }
    public SettingsDto SettingsDto { get; set; } = null!;
    public SettingsDto CurrentSettings { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        SettingsDto = await SettingsService.GetSettings();
        CurrentSettings = await SettingsService.GetSettings();
        IsLoading = false;
    }

    private bool HasChanges => JsonConvert.SerializeObject(SettingsDto) != JsonConvert.SerializeObject(CurrentSettings);

    private void ResetChanges()
    {
        SettingsDto = new SettingsDto
        {
            DailyRate = CurrentSettings.DailyRate,
            ExpectedMonthlyIncome = CurrentSettings.ExpectedMonthlyIncome,
        };

        StateHasChanged();
    }

    private async Task SaveChanges()
    {
        IsLoading = true;

        await SettingsService.UpdateSettings(SettingsDto);

        CurrentSettings = new SettingsDto
        {
            DailyRate = SettingsDto.DailyRate,
            ExpectedMonthlyIncome = SettingsDto.ExpectedMonthlyIncome,
        };
        IsLoading = false;
    }
}
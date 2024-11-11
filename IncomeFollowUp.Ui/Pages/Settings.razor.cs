using IncomeFollowUp.Contract;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;

namespace IncomeFollowUp.Ui.Pages;

public partial class Settings
{
    [Inject]
    public required MonthlyOutcomeService MonthlyOutcomeService { get; set; }

    [Inject]
    public required SettingsService SettingsService { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    public bool IsLoading { get; set; }
    public SettingsDto SettingsDto { get; set; } = null!;
    public SettingsDto CurrentSettings { get; set; } = null!;
    public MonthlyOutcomeDto[] MonthlyOutcomes { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        
        MonthlyOutcomes = await MonthlyOutcomeService.GetMonthlyOutcomes();
        SettingsDto = await SettingsService.GetSettings();
        
        CurrentSettings = new SettingsDto
        {
            DailyRate = SettingsDto.DailyRate,
            ExpectedMonthlyIncome = SettingsDto.ExpectedMonthlyIncome,
        };        
        
        IsLoading = false;
    }

    private bool HasChanges => JsonConvert.SerializeObject(SettingsDto) != JsonConvert.SerializeObject(CurrentSettings);

    private void ResetSettings()
    {
        SettingsDto = new SettingsDto
        {
            DailyRate = CurrentSettings.DailyRate,
            ExpectedMonthlyIncome = CurrentSettings.ExpectedMonthlyIncome,
        };

        StateHasChanged();
    }

    private async Task SaveSettings()
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
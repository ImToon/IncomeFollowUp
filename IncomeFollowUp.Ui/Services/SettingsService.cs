using System.Net.Http.Json;
using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Services;

public class SettingsService(HttpClient httpClient)
{
    private const string BASE_URL = "api/Settings";

    public async Task<SettingsDto> GetSettings()
    {
        var settingsDto = await httpClient.GetFromJsonAsync<SettingsDto>($"{BASE_URL}") ?? throw new Exception("An error occured while getting settings.");
        return settingsDto;
    }

    public async Task UpdateSettings(SettingsDto settingsDto)
    {
        var response = await httpClient.PutAsJsonAsync($"{BASE_URL}", settingsDto) ?? throw new Exception("An error occured while updating settings.");
        response.EnsureSuccessStatusCode();
    }
}
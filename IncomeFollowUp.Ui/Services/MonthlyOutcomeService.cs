using System.Net.Http.Json;
using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Services;

public class MonthlyOutcomeService(HttpClient httpClient)
{
    private const string BASE_URL = "api/MonthlyOutcomes";

    public async Task<MonthlyOutcomeDto[]> GetMonthlyOutcomes()
    {
        var monthlyOutcomes = await httpClient.GetFromJsonAsync<MonthlyOutcomeDto[]>($"{BASE_URL}") ?? throw new Exception("An error occured while getting monthly outcomes.");
        return monthlyOutcomes;
    }

    public async Task<MonthlyOutcomeDto> CreateMonthlyOutcome(MonthlyOutcomeDto monthlyOutcomeDto)
    {
        var response = await httpClient.PostAsJsonAsync($"{BASE_URL}", monthlyOutcomeDto) ?? throw new Exception("An error occured while creating monthly outcome.");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MonthlyOutcomeDto>();
    }

    public async Task DeleteMonthlyOutcome(Guid id)
    {
        var response = await httpClient.DeleteAsync($"{BASE_URL}/{id}") ?? throw new Exception("An error occured while deleting monthly outcome.");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateMonthlyOutcome(MonthlyOutcomeDto monthlyOutcomeDto)
    {
        var response = await httpClient.PutAsJsonAsync($"{BASE_URL}/{monthlyOutcomeDto.Id}", monthlyOutcomeDto) ?? throw new Exception("An error occured while updating monthly outcome.");
        response.EnsureSuccessStatusCode();
    }
}
using System.Net.Http.Json;
using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Services;

public class WorkDaysService(HttpClient httpClient)
{
    private const string BASE_URL = "api/WorkDays";

    public async Task<IEnumerable<WorkDaysSummaryDto>> GetSummary(SummaryType summaryType, int? year = null)
    {
        var workDaysSummaryDto = await httpClient.GetFromJsonAsync<IEnumerable<WorkDaysSummaryDto>>($"{BASE_URL}/summary?summaryType={summaryType}&year={year}") ?? throw new Exception("An error occured while getting work days summary.");
        return workDaysSummaryDto;
    }

    public async Task<IEnumerable<WorkDayDto>> GetWorkDays(int year, int month)
    {
        var workDaysDto = await httpClient.GetFromJsonAsync<IEnumerable<WorkDayDto>>($"{BASE_URL}?year={year}&month={month}") ?? throw new Exception("An error occured while getting work days.");
        return workDaysDto;
    }

    public async Task UpdateWorkDay(IEnumerable<UpdateWorkDayDto> updateWorkDayDtos)
    {
        var response = await httpClient.PutAsJsonAsync($"{BASE_URL}", updateWorkDayDtos) ?? throw new Exception("An error occured while updating work days.");
        response.EnsureSuccessStatusCode();
    }

    public async Task<int> GetExtraDays()
    {
        var extraDays = await httpClient.GetFromJsonAsync<int?>($"{BASE_URL}/extradays") ?? throw new Exception("An error occured while getting extra days.");
        return extraDays;
    }

    public async Task GenerateMonth(int month, int year)
    {
        var response = await httpClient.PostAsJsonAsync<IEnumerable<WorkDayDto>?>($"{BASE_URL}/{year}/{month}", null) ?? throw new Exception("An error occured while generating month.");
        response.EnsureSuccessStatusCode();
    }
}
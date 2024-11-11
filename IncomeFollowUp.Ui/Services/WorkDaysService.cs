using System.Net.Http.Json;
using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Services;

public class WorkDaysService(HttpClient httpClient)
{
    private const string BASE_URL = "api/WorkDays";

    public async Task<MonthlyWorkDaysSummaryDto[]> GetLatestsMonthsSummary()
    {
        var monthlyWorkDaysSummariesDto = await httpClient.GetFromJsonAsync<MonthlyWorkDaysSummaryDto[]>($"{BASE_URL}/summary") ?? throw new Exception("An error occured while getting monthly work days summary.");
        return monthlyWorkDaysSummariesDto;
    }

    public async Task<YearlyWorkDaysSummaryDto> GetYearlySummary(int year)
    {
        var yearlyWorkDaysSummariesDto = await httpClient.GetFromJsonAsync<YearlyWorkDaysSummaryDto>($"{BASE_URL}/summary/year/{year}") ?? throw new Exception("An error occured while getting early work days summary.");
        return yearlyWorkDaysSummariesDto;
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

    public async Task<int> GetExtraDays(int year)
    {
        var extraDays = await httpClient.GetFromJsonAsync<int?>($"{BASE_URL}/extradays/year/{year}") ?? throw new Exception("An error occured while getting extra days.");
        return extraDays;
    }

    public async Task GenerateMonth(int year, int month)
    {
        var response = await httpClient.PostAsJsonAsync<IEnumerable<WorkDayDto>?>($"{BASE_URL}/{year}/{month}", null) ?? throw new Exception("An error occured while generating month.");
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMonth(int year, int month)
    {
        var response = await httpClient.DeleteAsync($"{BASE_URL}/{year}/{month}") ?? throw new Exception("An error occured while deleting month.");
        response.EnsureSuccessStatusCode();
    }
}
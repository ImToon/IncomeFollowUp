using System.Net.Http.Json;
using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Services;

public class WorkDaysService(HttpClient httpClient)
{
    private const string BASE_URL = "api/WorkDays";

    public async Task<IEnumerable<WorkDaysSummaryDto>> GetSummary()
    {
        var workDaysSummaryDto = await httpClient.GetFromJsonAsync<IEnumerable<WorkDaysSummaryDto>>($"{BASE_URL}/summary") ?? throw new Exception("An error occured while getting work days summary.");
        return workDaysSummaryDto;
    }
}
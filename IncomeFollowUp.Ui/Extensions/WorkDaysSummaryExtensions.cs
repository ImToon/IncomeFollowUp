using IncomeFollowUp.Contract;

namespace IncomeFollowUp.Ui.Extensions;

public static class WorkDaysSummaryDtoExtensions
{
    public static string GetRow(this WorkDaysSummaryDto workDaysSummaryDto)
    {
        return $"{workDaysSummaryDto.Date:MM/yyyy} {workDaysSummaryDto.Total}€";
    }
}
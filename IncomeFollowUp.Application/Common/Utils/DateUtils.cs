namespace IncomeFollowUp.Application.Common.Utils;

public class DateUtils
{
    public static List<DateTime> GetWeekdaysOfMonth(int year, int month)
    {
        List<DateTime> weekdays = [];
        int daysInMonth = DateTime.DaysInMonth(year, month);

        for (int day = 1; day <= daysInMonth; day++)
        {
            DateTime currentDate = new(year, month, day);
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdays.Add(currentDate);
            }
        }

        return weekdays;
    }
}
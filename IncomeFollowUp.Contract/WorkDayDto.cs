namespace IncomeFollowUp.Contract;

public record WorkDayDto(Guid? Id, DateTime Date, bool IsWorkDay, int DailyRate);

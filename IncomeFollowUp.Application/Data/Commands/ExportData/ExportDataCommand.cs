using MediatR;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.Data.Commands.ExportData;

public record ExportDataCommand : IRequest<ExportDataResult>;

public class ExportDataResult
{
    public List<Domain.Settings>? Settings { get; set; }
    public List<MonthlyOutcome>? MonthlyOutcomes { get; set; }
    public List<WorkDay>? WorkDays { get; set; }
    public List<MonthlyIncome>? MonthlyIncomes { get; set; }
}

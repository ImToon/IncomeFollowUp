using MediatR;
using System.Collections.Generic;
using IncomeFollowUp.Domain;

namespace IncomeFollowUp.Application.Data.Commands.ImportData;

public class ImportDataCommand : IRequest<Unit>
{
    public List<Domain.Settings>? Settings { get; set; }
    public List<MonthlyOutcome>? MonthlyOutcomes { get; set; }
    public List<MonthlyIncome>? MonthlyIncomes { get; set; }
}

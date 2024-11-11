using MediatR;

namespace IncomeFollowUp.Application.WorkDays.Queries.GetVacation;

public class GetExtraDaysQuery : IRequest<int>
{
    public int Year { get; set; }
}
using IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;
using IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;
using IncomeFollowUp.Application.WorkDays.Queries.GetVacation;
using IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;
using IncomeFollowUp.Application.WorkDays.Queries.GetWorkDaysSummary;
using IncomeFollowUp.Contract;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncomeFollowUp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkDaysController(ISender sender, IMapper mapper) : ControllerBase
{
    [HttpGet("{year:int}/{month:int}")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<WorkDayDto>))]
    public async Task<IActionResult> GetWorkDays(int year, int month)
    {
        var workDays = await sender.Send(new GetWorkDaysQuery { Year = year, Month = month });
        return Ok(mapper.Map<IEnumerable<WorkDayDto>>(workDays));
    }

    [HttpGet("summary")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<WorkDaysSummaryDto>))]
    public async Task<IActionResult> GetSummary()
    {
        var workDaysSummary = await sender.Send(new GetWorkDaysSummaryQuery());
        return Ok(mapper.Map<IEnumerable<WorkDaysSummaryDto>>(workDaysSummary));
    }

    [HttpGet("vacation")]
    [ProducesResponseType(statusCode: 200, type: typeof(int))]
    public async Task<IActionResult> GetVacation()
    {
        var vacation = await sender.Send(new GetVacationQuery());
        return Ok(vacation);
    }

    [HttpPost("{year:int}/{month:int}")]
    [ProducesResponseType(statusCode: 201, type: typeof(IEnumerable<WorkDayDto>))]
    public async Task<IActionResult> GenerateMonth(int year, int month)
    {
        var workDays = await sender.Send(new GenerateMonthCommand { Year = year, Month = month });
        return CreatedAtAction(nameof(GetWorkDays), new { Year = year, Month = month }, mapper.Map<IEnumerable<WorkDayDto>>(workDays));
    }

    [HttpPut]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<WorkDayDto>))]
    public async Task<IActionResult> UpdateWorkDays([FromBody]IEnumerable<UpdateWorkDayDto> workDaysUpdates)
    {
        var updateWorkDayCommands = mapper.Map<IEnumerable<UpdateWorkDayCommand>>(workDaysUpdates);
        var workDays = await sender.Send(new UpdateWorkDaysCommand { UpdateWorkDayCommands = updateWorkDayCommands });
        return Ok(mapper.Map<IEnumerable<WorkDayDto>>(workDays));
    }
}
using IncomeFollowUp.Application.WorkDays.Commands.DeleteMonth;
using IncomeFollowUp.Application.WorkDays.Commands.GenerateMonth;
using IncomeFollowUp.Application.WorkDays.Commands.UpdateWorkDays;
using IncomeFollowUp.Application.WorkDays.Queries.GetVacation;
using IncomeFollowUp.Application.WorkDays.Queries.GetWorkDays;
using IncomeFollowUp.Application.WorkDays.Queries.GetMonthlyWorkDaysSummaries;
using IncomeFollowUp.Application.WorkDays.Queries.GetYearlyWorkDaysSummary;
using IncomeFollowUp.Contract;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncomeFollowUp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkDaysController(ISender sender, IMapper mapper) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<WorkDayDto>))]
    public async Task<IActionResult> GetWorkDays([FromQuery]int year, [FromQuery]int month)
    {
        var workDays = await sender.Send(new GetWorkDaysQuery { Year = year, Month = month });
        return Ok(mapper.Map<IEnumerable<WorkDayDto>>(workDays));
    }

    [HttpGet("summary/year/{year:int}")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<YearlyWorkDaysSummaryDto>))]
    public async Task<IActionResult> GetYearlySummary(int year)
    {
        var yearlyWorkDaysSummaryDto = await sender.Send(new GetYearlyWorkDaysSummaryQuery { Year = year });
        return Ok(mapper.Map<YearlyWorkDaysSummaryDto>(yearlyWorkDaysSummaryDto));
    }

    [HttpGet("summary")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<MonthlyWorkDaysSummaryDto[]>))]
    public async Task<IActionResult> GetLatestsMonthsSummary([FromQuery] int? months)
    {
        var workDaysSummary = await sender.Send(new GetMonthlyWorkDaysSummariesQuery(months));
        return Ok(mapper.Map<MonthlyWorkDaysSummaryDto[]>(workDaysSummary));
    }

    [HttpGet("extradays/year/{year:int}")]
    [ProducesResponseType(statusCode: 200, type: typeof(int))]
    public async Task<IActionResult> GetExtraDays(int year)
    {
        var vacation = await sender.Send(new GetExtraDaysQuery{ Year = year });
        return Ok(vacation);
    }

    [HttpPost("{year:int}/{month:int}")]
    [ProducesResponseType(statusCode: 201, type: typeof(IEnumerable<WorkDayDto>))]
    public async Task<IActionResult> GenerateMonth(int year, int month)
    {
        var workDays = await sender.Send(new GenerateMonthCommand { Year = year, Month = month });
        return CreatedAtAction(nameof(GetWorkDays), new { Year = year, Month = month }, mapper.Map<IEnumerable<WorkDayDto>>(workDays));
    }

    [HttpDelete("{year:int}/{month:int}")]
    [ProducesResponseType(statusCode: 204)]
    public async Task<IActionResult> DeleteMonth(int year, int month)
    {
        await sender.Send(new DeleteMonthCommand { Year = year, Month = month });
        return NoContent();
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
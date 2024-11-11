using IncomeFollowUp.Application.MonthlyOutcomes.Commands.CreateMonthlyOutcome;
using IncomeFollowUp.Application.MonthlyOutcomes.Commands.DeleteMonthlyOutcome;
using IncomeFollowUp.Application.MonthlyOutcomes.Commands.UpdateMonthlyOutcome;
using IncomeFollowUp.Application.MonthlyOutcomes.Queries.GetMonthlyOutcomes;
using IncomeFollowUp.Contract;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncomeFollowUp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MonthlyOutcomesController(ISender sender, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 200, type: typeof(MonthlyOutcomeDto[]))]
    public async Task<IActionResult> GetMonthlyOutcomes()
    {
        var monthlyOutcomes = await sender.Send(new GetMonthlyOutcomesQuery());
        return Ok(mapper.Map<MonthlyOutcomeDto[]>(monthlyOutcomes));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 201, type: typeof(MonthlyOutcomeDto))]
    public async Task<IActionResult> CreateMonthlyOutcome([FromBody] MonthlyOutcomeDto monthlyOutcome)
    {
        var createdMonthlyOutcome = await sender.Send(mapper.Map<CreateMonthlyOutcomeCommand>(monthlyOutcome));

        return CreatedAtAction(nameof(CreateMonthlyOutcome), mapper.Map<MonthlyOutcomeDto>(createdMonthlyOutcome));
    }

    [HttpPut("{id:Guid}")]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 200, type: typeof(MonthlyOutcomeDto))]    
    public async Task<IActionResult> UpdateMonthlyOutcome(Guid id, [FromBody] MonthlyOutcomeDto monthlyOutcome)
    {
        var updatedMonthlyOutcome = await sender.Send(new UpdateMonthlyOutcomeCommand { Id = id, Name = monthlyOutcome.Name, Amount = monthlyOutcome.Amount });
        return Ok(mapper.Map<MonthlyOutcomeDto>(updatedMonthlyOutcome));
    }

    [HttpDelete("{id:Guid}")]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 204)]
    public async Task<IActionResult> DeleteMonthlyOutcome(Guid id)
    {
        await sender.Send(new DeleteMonthlyOutcomeCommand{ Id = id});
        return NoContent();
    }
}
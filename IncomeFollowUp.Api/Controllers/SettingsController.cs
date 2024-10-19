using IncomeFollowUp.Application.Settings.Commands.CreateSettings;
using IncomeFollowUp.Application.Settings.Commands.UpdateSettings;
using IncomeFollowUp.Application.Settings.Queries.GetSettings;
using IncomeFollowUp.Contract;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncomeFollowUp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SettingsController(ISender sender, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 200, type: typeof(SettingsDto))]
    public async Task<IActionResult> GetSettings()
    {
        Domain.Settings? settings = await sender.Send(new GetSettingsQuery());
        return Ok(mapper.Map<SettingsDto?>(settings));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 201, type: typeof(SettingsDto))]
    public async Task<IActionResult> CreateSettings([FromBody] SettingsDto settings)
    {
        var createdSettings = await sender.Send(mapper.Map<CreateSettingsCommand>(settings));
        return CreatedAtAction(nameof(GetSettings), mapper.Map<SettingsDto>(createdSettings));
    }

    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(statusCode: 200, type: typeof(SettingsDto))]
    public async Task<IActionResult> UpdateSettings([FromBody] SettingsDto settings)
    {
        var updatedSettings = await sender.Send(mapper.Map<UpdateSettingsCommand>(settings));
        return Ok(mapper.Map<SettingsDto>(updatedSettings));
    }
}
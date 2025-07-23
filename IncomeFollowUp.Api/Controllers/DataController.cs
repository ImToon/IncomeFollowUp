using Microsoft.AspNetCore.Mvc;
using MediatR;
using IncomeFollowUp.Contract;
using MapsterMapper;
using IncomeFollowUp.Application.Data.Commands.ExportData;
using IncomeFollowUp.Application.Data.Commands.ImportData;
using IncomeFollowUp.Application.Data.Commands.ClearData;

namespace IncomeFollowUp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController(IMapper mapper, ISender sender) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;

    [HttpGet("export")]
    public async Task<IActionResult> Export()
    {
        var result = await _sender.Send(new ExportDataCommand());
        return Ok(_mapper.Map<ExportDataDto>(result));
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import([FromBody] ImportDataDto data)
    {
        await _sender.Send(_mapper.Map<ImportDataCommand>(data));
        return Ok();
    }

    [HttpPost("clear")]
    public async Task<IActionResult> Clear()
    {
        await _sender.Send(new ClearDataCommand());
        return Ok();
    }
}
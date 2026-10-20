using System.ComponentModel.DataAnnotations;
using CalcApiControllers.History;
using Microsoft.AspNetCore.Mvc;

namespace CalcApiControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoryController : ControllerBase
{
    private readonly ICalculationHistory _history;

    public HistoryController(ICalculationHistory history)
    {
        _history = history;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CalculationRecord>>> GetLatest([Range(1, 100)] int limit = 10)
    {
        return Ok(await _history.GetLatestAsync(limit));
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<CalculationRecord>> Get(long id)
    {
        var record = await _history.GetAsync(id);
        return record is null ? NotFound() : record;
    }

    [HttpDelete]
    [Route("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        return await _history.DeleteAsync(id) ? NoContent() : NotFound();
    }
}

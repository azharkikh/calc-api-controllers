using Microsoft.AspNetCore.Mvc;

namespace CalcApiControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly Calculator _calculator;

    public CalculatorController(Calculator calculator)
    {
        _calculator = calculator;
    }

    [HttpGet]
    [Route("add")]
    public ActionResult<double> AddNumbers(double a, double b)
    {
        return _calculator.Add(a, b);
    }
}

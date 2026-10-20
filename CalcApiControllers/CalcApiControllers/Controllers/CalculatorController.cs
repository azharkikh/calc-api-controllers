using CalcApiControllers.History;
using CalcApiControllers.Validation;
using Microsoft.AspNetCore.Mvc;

namespace CalcApiControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly Calculator _calculator;
    private readonly ICalculationHistory _history;

    public CalculatorController(Calculator calculator, ICalculationHistory history)
    {
        _calculator = calculator;
        _history = history;
    }

    [HttpGet]
    [Route("add")]
    public async Task<ActionResult<double>> AddNumbers(double a, double b)
    {
        var result = _calculator.Add(a, b);
        await SaveToHistory("add", a, b, result);
        return result;
    }

    [HttpGet]
    [Route("multiply")]
    public async Task<ActionResult<double>> MultiplyNumbers(double a, double b)
    {
        var result = _calculator.Multiply(a, b);
        await SaveToHistory("multiply", a, b, result);
        return result;
    }

    [HttpGet]
    [Route("divide")]
    public async Task<ActionResult<double>> DivideNumbers(double a, [NotZero] double b)
    {
        var result = _calculator.Divide(a, b);
        await SaveToHistory("divide", a, b, result);
        return result;
    }

    [HttpGet]
    [Route("subtract")]
    public async Task<ActionResult<double>> SubtractNumbers(double a, double b)
    {
        var result = _calculator.Subtract(a, b);
        await SaveToHistory("subtract", a, b, result);
        return result;
    }

    private async Task SaveToHistory(string operation, double a, double b, double result)
    {
        var record = await _history.AddAsync(operation, a, b, result);
        Response.Headers["X-Calculation-Id"] = record.Id.ToString();
    }
}

public class ExtendedCalculatorController : ControllerBase
{
    private readonly ExtendedCalculator _extendedCalculator;
    private readonly ICalculationHistory _history;

    public ExtendedCalculatorController(ExtendedCalculator extendedCalculator, ICalculationHistory history)
    {
        _extendedCalculator = extendedCalculator;
        _history = history;
    }
    
    [HttpGet]
    [Route("calculatePower")]
    public async Task<ActionResult<double>> CalculatePower(double a, double b)
    {
        var result = _extendedCalculator.CalculatePower(a, b);
        var record = await _history.AddAsync("power", a, b, result);
        Response.Headers["X-Calculation-Id"] = record.Id.ToString();
        return result;
    }
}

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

    [HttpGet]
    [Route("multiply")]
    public ActionResult<double> MultiplyNumbers(double a, double b)
    {
        return _calculator.Multiply(a, b);
    }

    [HttpGet]
    [Route("divide")]
    public ActionResult<double> DivideNumbers(double a, double b)
    {
        return _calculator.Divide(a, b);
    }

    [HttpGet]
    [Route("subtract")]
    public ActionResult<double> SubtractNumbers(double a, double b)
    {
        return _calculator.Subtract(a, b);
    }
}

public class ExtendedCalculatorController : ControllerBase
{
    private readonly ExtendedCalculator _extendedCalculator;

    public ExtendedCalculatorController(ExtendedCalculator extendedCalculator)
    {
        _extendedCalculator = extendedCalculator;
    }
    
    [HttpGet]
    [Route("calculatePower")]
    public ActionResult<double> CalculatePower(double a, double b)
    {
        return _extendedCalculator.CalculatePower(a, b);
    }
}

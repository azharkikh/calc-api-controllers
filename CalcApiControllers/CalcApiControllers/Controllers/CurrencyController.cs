using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CalcApiControllers.Currency;
using Microsoft.AspNetCore.Mvc;

namespace CalcApiControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyConverter _converter;

    public CurrencyController(CurrencyConverter converter)
    {
        _converter = converter;
    }

    [HttpGet]
    [Route("convert")]
    public async Task<ActionResult<double>> Convert(
        [Range(0, double.MaxValue)] double amount,
        [RegularExpression("^[A-Za-z]{3}$")] string from,
        [RegularExpression("^[A-Za-z]{3}$")] string to)
    {
        try
        {
            return await _converter.ConvertAsync(amount, from, to);
        }
        catch (UnknownCurrencyException ex)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: ex.Message);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            return Problem(statusCode: StatusCodes.Status504GatewayTimeout, title: "Провайдер курсов не ответил вовремя.");
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException)
        {
            return Problem(statusCode: StatusCodes.Status502BadGateway, title: "Провайдер курсов недоступен.");
        }
    }
}

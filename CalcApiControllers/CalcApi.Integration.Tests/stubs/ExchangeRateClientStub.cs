using CalcApiControllers.Currency;

namespace CalcApi.Integration.Tests.stubs;

public class ExchangeRateClientStub : IExchangeRateClient
{
    public Task<double?> GetRateAsync(string from, string to)
    {
        return Task.FromResult<double?>(20);
    }
}
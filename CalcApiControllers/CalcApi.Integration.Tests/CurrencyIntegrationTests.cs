using System.Net;
using CalcApi.Integration.Tests.stubs;
using CalcApiControllers;
using CalcApiControllers.Currency;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;

namespace CalcApi.Integration.Tests;

[TestFixture]

public class CurrencyIntegrationTests
{
    private readonly WebApplicationFactory<Program> _program;
     
    public CurrencyIntegrationTests()
    {
        _program = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IExchangeRateClient>();
                services.AddSingleton<IExchangeRateClient, ExchangeRateClientStub>();
            }));
    }
         
    [OneTimeTearDown]
    public void OneTimeTearDownAttribute()
    {
        _program.Dispose();
    }
          
    [Test]
    public async Task CurrencyConverter_UnknownCurrency_BadRequest(){
       
       var client = _program.CreateClient();
       
       var result = await client.GetAsync($"/Currency/convert?amount=2&from=rub&to=TRY");

       Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest)); 
    }
    
    [Test]
    public async Task CurrencyConverter_RightCurrency_Ok(){
       
        var client = _program.CreateClient();
       
        var result = await client.GetAsync($"/Currency/convert?amount=2&from=rub&to=TRY");

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK)); 
    }
    
    [Test]
    public async Task CurrencyConverter_Result_(){
       
        var client = _program.CreateClient();
       
        var result = await client.GetAsync($"/Currency/convert?amount=2&from=rub&to=TRY");

      //  Assert.That(result.StatusCode, Is.EqualTo()); 
    }
    
         
}
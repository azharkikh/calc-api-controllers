using NUnit.Framework;
using Testcontainers.MySql;

namespace CalcApi.Integration.Tests;

public abstract class IntegrationTestBase
{
    private readonly MySqlContainer _mysql; 
    
    protected IntegrationTestBase()
    {
        _mysql = new MySqlBuilder("mysql:8.4").Build();
    }

    [OneTimeSetUp]
    public async Task StartDatabaseAsync()
    {
        await _mysql.StartAsync();
    }

    [OneTimeTearDown]
    public async Task StopDatabaseAsync()
    {
        await _mysql.DisposeAsync();
    }
}
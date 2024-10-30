using System.Runtime.ConstrainedExecution;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

[TestFixture]
public class AppConfigTest
{
    private const string BASE_CURRENCY = "USD";
    private const string TARGET_CURRENCY = "EUR";
    private const string EXCHANGE_API_PAIR_URL = "https://v6.exchangerate-api.com/v6/%s/pair/%s/%s";
    private const string EXCHANGE_API_STANDARD_URL = "https://v6.exchangerate-api.com/v6/%s/latest/%s";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGetExchangeApiPairUrl()
    {
        AppConfig appConfig = new AppConfig();
        Assert.IsNotEmpty(appConfig.ApiKey);
        Console.WriteLine(appConfig.ApiKey);
        Assert.That(appConfig.ExchangeApiStandardUrl, Is.EqualTo(EXCHANGE_API_STANDARD_URL));
        Console.WriteLine(appConfig.ExchangeApiStandardUrl);
        Assert.That(appConfig.ExchangeApiPairUrl, Is.EqualTo(EXCHANGE_API_PAIR_URL));
        Console.WriteLine(appConfig.ExchangeApiPairUrl);
    }

}
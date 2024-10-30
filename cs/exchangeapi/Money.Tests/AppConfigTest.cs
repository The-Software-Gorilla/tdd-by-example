using System.Runtime.ConstrainedExecution;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

[TestFixture]
public class AppConfigTest
{
    private const string BASE_CURRENCY = "USD";
    private const string TARGET_CURRENCY = "EUR";
    private const string EXCHANGE_API_PAIR_URL = "https://v6.exchangerate-api.com/v6/{0}/pair/{1}/{2}";
    private const string EXCHANGE_API_STANDARD_URL = "https://v6.exchangerate-api.com/v6/{0}/latest/{1}";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGetExchangeApiPairUrl()
    {
        AppConfig appConfig = new AppConfig();
        Assert.IsNotEmpty(appConfig.ApiKey);
        Assert.That(appConfig.ExchangeApiStandardUrl, Is.EqualTo(EXCHANGE_API_STANDARD_URL));
        Assert.That(appConfig.ExchangeApiPairUrl, Is.EqualTo(EXCHANGE_API_PAIR_URL));
    }

}
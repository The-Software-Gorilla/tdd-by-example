using System.Runtime.CompilerServices;
using System.Text.Json;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;
using TheSoftwareGorilla.TDD.Money.Http;
using TheSoftwareGorilla.TDD.Money.Json;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class ExchangeRateApiTest
{
    public const string BASE_CURRENCY = "USD";
    public const string TARGET_CURRENCY = "EUR";


    private static readonly string API_KEY_SETTING;
    private static readonly string API_PAIR_URL_SETTING;
    private static readonly string API_STANDARD_URL_SETTING;
    private static readonly AppConfig APP_SETTINGS;
    private static readonly string PAIR_RESPONSE_FILE;
    private static readonly string STANDARD_RESPONSE_FILE;

    static ExchangeRateApiTest() {
        APP_SETTINGS = new AppConfig();
        API_KEY_SETTING = APP_SETTINGS.ApiKey;
        API_PAIR_URL_SETTING = APP_SETTINGS.ExchangeApiPairUrl;
        API_STANDARD_URL_SETTING = APP_SETTINGS.ExchangeApiStandardUrl;
        PAIR_RESPONSE_FILE = AppConfig.GetSolutionPathForFile("pair_response.json");
        STANDARD_RESPONSE_FILE = AppConfig.GetSolutionPathForFile("standard_response.json");
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestHttpGetStandard() {
        GetRequest request = new GetRequest(String.Format(API_STANDARD_URL_SETTING, API_KEY_SETTING, BASE_CURRENCY));   
        string response = request.FetchResponseAsync().Result;
        Assert.IsNotNull(response);
    }

    [Test]
    public void TestHttpGetPair() {
        GetRequest request = new GetRequest(String.Format(API_PAIR_URL_SETTING, API_KEY_SETTING, BASE_CURRENCY, TARGET_CURRENCY));   
        string response = request.FetchResponseAsync().Result;
        Assert.IsNotNull(response);
    }

    [Test]
    public void TestDeserializePairResponse() {
        string jsonString = File.ReadAllText(PAIR_RESPONSE_FILE);
        ExchangeRateApiPairResponse response = JsonSerializer.Deserialize<ExchangeRateApiPairResponse>(jsonString);
        Assert.IsNotNull(response);
        Assert.That(response.BaseCode, Is.EqualTo(BASE_CURRENCY));
        Assert.That(response.TargetCode, Is.EqualTo(TARGET_CURRENCY));
        Assert.IsNotNull(response.ConversionRate);
        Assert.That(response.ConversionRate, Is.GreaterThan(0));
    }

    [Test]
    public void TestDeserializeStandardResponse() {
        string jsonString = File.ReadAllText(STANDARD_RESPONSE_FILE);
        ExchangeRateApiStandardResponse response = JsonSerializer.Deserialize<ExchangeRateApiStandardResponse>(jsonString);
        Assert.IsNotNull(response);
        Assert.That(response.BaseCode, Is.EqualTo(BASE_CURRENCY));
        Assert.IsNotNull(response.ConversionRates);
        Assert.That(response.ConversionRates.Count, Is.GreaterThan(0));
        Assert.That(response.ConversionRates.ContainsKey(TARGET_CURRENCY));
        Assert.That(response.ConversionRates[TARGET_CURRENCY], Is.GreaterThan(0));
    }

    [Test]
    public void TestExchangeRateApiFetcher() {
        ExchangeRateApiFetcher fetcher = new ExchangeRateApiFetcher(APP_SETTINGS, BASE_CURRENCY).Fetch();
        Assert.IsNotNull(fetcher);
        Assert.That(fetcher.BaseCurrency, Is.EqualTo(BASE_CURRENCY));
        Assert.IsNotNull(fetcher.TimeLastUpdateUtc);
        Assert.IsNotNull(fetcher.ConversionRates);
        Assert.IsNotNull(fetcher.ConversionRates[TARGET_CURRENCY]);
        Assert.That(fetcher.ConversionRates[TARGET_CURRENCY], Is.GreaterThan(0));
    }  
}
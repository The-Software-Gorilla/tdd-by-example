using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;
using TheSoftwareGorilla.TDD.Money.Http;
using TheSoftwareGorilla.TDD.Money.JsonHolder;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
//TODO: Refactor tests to segregate each class into its own test class
//TODO: Add a CurrencyExchange object to cache all exchange rates and lazy load them as needed.
//TODO: Get to 100% code coverage.
//TODO: Create the utility class for the JSON parsing.
//TODO: Create a utility class for the HTTP requests.
//TODO: Build solution for error responses.
#endregion

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
    private static readonly DateTime TEST_DATE_STANDARD;

    static ExchangeRateApiTest() {
        APP_SETTINGS = new AppConfig();
        API_KEY_SETTING = APP_SETTINGS.ApiKey;
        API_PAIR_URL_SETTING = APP_SETTINGS.ExchangeApiPairUrl;
        API_STANDARD_URL_SETTING = APP_SETTINGS.ExchangeApiStandardUrl;
        PAIR_RESPONSE_FILE = AppConfig.GetSolutionPathForFile("pair_response.json");
        STANDARD_RESPONSE_FILE = AppConfig.GetSolutionPathForFile("standard_response.json");
        TEST_DATE_STANDARD = DateTime.ParseExact("Wed, 30 Oct 2024 00:00:01 +0000", "ddd, dd MMM yyyy HH:mm:ss K", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
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
        Assert.IsNotNull(response.TimeLastUpdateUtc);
        DateTime utcDate = GetDateFromStringUtc(response.TimeLastUpdateUtc).Value;
        Assert.That(utcDate, Is.EqualTo(TEST_DATE_STANDARD));
        Assert.IsNotNull(response.TimeLastUpdateUnix);
        DateTime unixDate = GetDateFromUnixTimestamp(response.TimeLastUpdateUnix.Value);
        Assert.That(unixDate, Is.EqualTo(TEST_DATE_STANDARD));
        Assert.That(utcDate, Is.EqualTo(unixDate));
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

    private static DateTime GetLocalDateFromUtcDate(DateTime dateTime) 
    {
        DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
        return localDate;
    }

    private static DateTime GetUtcDateFromLocalDate(DateTime dateTime) 
    {
        DateTime utcDate = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Utc);
        return utcDate;
    }

    private static DateTime GetLocalDateFromUnixTimestampUtc(int timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        DateTime localDateTime = dateTimeOffset.LocalDateTime;
        return localDateTime;
    }
    private static DateTime GetDateFromUnixTimestamp(int timestamp)
    {
        DateTime response = DateTime.UnixEpoch.AddSeconds(timestamp);
        return response;
    }

    private static DateTime? GetDateFromStringUtc(string dateString) {
        const string format = "ddd, dd MMM yyyy HH:mm:ss K";
        try
        {
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            return date;
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }       

    private static DateTime? GetLocalDateFromStringUtc(string dateString) {
        const string format = "ddd, dd MMM yyyy HH:mm:ss K";
        var localTimeZone = TimeZoneInfo.Local;

        try
        {
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(date, localTimeZone);
            return localDate;
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }       
}
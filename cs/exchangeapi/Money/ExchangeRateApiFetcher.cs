using System.Text.Json;
using TheSoftwareGorilla.TDD.Money.Http;
using TheSoftwareGorilla.TDD.Money.Json;

namespace TheSoftwareGorilla.TDD.Money;

public class ExchangeRateApiFetcher
{

    public string BaseCurrency { get; }
    private readonly string _apiKey;
    private readonly string _apiStandardUrl;

    private ExchangeRateApiStandardResponse _standardResponse;

    public ExchangeRateApiFetcher(AppConfig appConfig, string baseCurrency)
    {
        _apiKey = appConfig.ApiKey;
        _apiStandardUrl = appConfig.ExchangeApiStandardUrl;
        BaseCurrency = baseCurrency;
    }

    public ExchangeRateApiFetcher Fetch() {
        GetRequest request = new GetRequest(String.Format(_apiStandardUrl, _apiKey, BaseCurrency));   
        string response = request.FetchResponseAsync().Result;
        _standardResponse = JsonSerializer.Deserialize<ExchangeRateApiStandardResponse>(response);
        return this;
    }

    public string TimeLastUpdateUtc {
        get {
            return _standardResponse.TimeLastUpdateUtc;
        }
    }

    public Dictionary<string, double> ConversionRates {
        get {
            return _standardResponse.ConversionRates;
        }
    }

}

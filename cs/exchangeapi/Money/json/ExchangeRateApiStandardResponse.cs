using System;
using System.Text.Json.Serialization;


namespace TheSoftwareGorilla.TDD.Money.Json;
public class ExchangeRateApiStandardResponse
{
    private Dictionary<string, double> _conversionRates = new Dictionary<string, double>();

    [JsonPropertyName("result")]
    public string Result {get; set;}

    [JsonPropertyName("documentation")]
    public string Documentation {get; set;}

    [JsonPropertyName("terms_of_use")]
    public string TermsOfUse {get; set;}

    [JsonPropertyName("time_last_update_unix")]
    public int? TimeLastUpdateUnix {get; set;}

    [JsonPropertyName("time_last_update_utc")]
    public string TimeLastUpdateUtc {get; set;}

    [JsonPropertyName("time_next_update_unix")]
    public int? TimeNextUpdateUnix {get; set;}

    [JsonPropertyName("time_next_update_utc")]
    public string TimeNextUpdateUtc {get; set;}

    [JsonPropertyName("base_code")]
    public string BaseCode {get; set;}

    [JsonPropertyName("conversion_rates")]
    public Dictionary<string, double> ConversionRates
    {
        get => _conversionRates;
        set => _conversionRates = value;
    }
}

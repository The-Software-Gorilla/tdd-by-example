package com.thesoftwaregorilla.tdd.money;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.thesoftwaregorilla.tdd.money.http.GetRequest;
import com.thesoftwaregorilla.tdd.money.json.ExchangeApiStandardResponse;

import java.io.IOException;
import java.util.logging.Logger;

//<editor-fold desc="TO DO's">
//TODO: Change type of getTimeLastUpdateUtc() to UTCDateTime
//</editor-fold>


public class ExchangeApiFetcher {

    private final Logger logger = Logger.getLogger(ExchangeApiFetcher.class.getName());
    public static final String EXCHANGE_API_STANDARD_URL = "https://v6.exchangerate-api.com/v6/%s/latest/%s";
    private final String apiKey;
    private final String baseCurrency;
    private ExchangeApiStandardResponse rateResponse;
    public ExchangeApiFetcher(String apiKey, String baseCurrency) {
        this.apiKey = apiKey;
        this.baseCurrency = baseCurrency;
    }

    public ExchangeApiFetcher fetch() {
        GetRequest getRequest = new GetRequest(String.format(EXCHANGE_API_STANDARD_URL, apiKey, baseCurrency));
        rateResponse = parseResponse(getRequest.fetchResponse(), ExchangeApiStandardResponse.class);
        return this;
    }

    public String getBaseCurrency() {
        return baseCurrency;
    }

    public String getTimeLastUpdateUtc() {
        if (rateResponse == null) {
            return null;
        }
        return rateResponse.getTime_last_update_utc();
    }

    public boolean isRateExists(String targetCurrency) {
        if (rateResponse != null && rateResponse.getConversion_rates() != null) {
            return rateResponse.getConversion_rates().containsKey(targetCurrency);
        }
        return false;
    }

    public double getConversionRate(String targetCurrency) {
        if (rateResponse != null && rateResponse.getConversion_rates() != null) {
            return rateResponse.getConversion_rates().getOrDefault(targetCurrency, 0.0);
        }
        return 0.0;
    }

    private <T> T parseResponse(String response, Class<T> responseType) {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(response, responseType);
        } catch (IOException e) {
            logger.severe("Failed to parse response: " + e.getMessage());
            return null;
        }
    }

    @Override
    public String toString() {
        return "ExchangeApiFetcher{" +
                "baseCurrency='" + baseCurrency + '\'' +
                ", timeLastUpdateUtc='" + getTimeLastUpdateUtc() + '\'' +
                ", ZARRate='" + rateResponse.getConversion_rates().getOrDefault("ZAR", 0.0) + '\'' +
                '}';
    }


}

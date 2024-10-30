package com.thesoftwaregorilla.tdd.money;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.thesoftwaregorilla.tdd.money.http.GetRequest;
import com.thesoftwaregorilla.tdd.money.jsonholder.ExchangeRateApiStandardResponse;

import java.io.IOException;
import java.util.logging.Logger;

//<editor-fold desc="TO DO's">
//TODO: Change type of getTimeLastUpdateUtc() to UTCDateTime
//</editor-fold>


public class ExchangeRateApiFetcher {

    private final Logger logger = Logger.getLogger(ExchangeRateApiFetcher.class.getName());
    private final String apiStandardUrl;
    private final String apiKey;
    private final String baseCurrency;
    private ExchangeRateApiStandardResponse rateResponse;
    public ExchangeRateApiFetcher(AppConfig config, String baseCurrency) {
        this.apiKey = config.getApiKey();
        this.apiStandardUrl = config.getExchangeApiStandardUrl();
        this.baseCurrency = baseCurrency;
    }

    public ExchangeRateApiFetcher fetch() {
        GetRequest getRequest = new GetRequest(String.format(apiStandardUrl, apiKey, baseCurrency));
        rateResponse = parseResponse(getRequest.fetchResponse(), ExchangeRateApiStandardResponse.class);
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
        return "ExchangeRateApiFetcher{" +
                "baseCurrency='" + baseCurrency + '\'' +
                ", timeLastUpdateUtc='" + getTimeLastUpdateUtc() + '\'' +
                ", ZARRate='" + rateResponse.getConversion_rates().getOrDefault("ZAR", 0.0) + '\'' +
                '}';
    }


}

package com.thesoftwaregorilla.tdd.money;

import com.thesoftwaregorilla.tdd.money.http.GetRequest;
import com.thesoftwaregorilla.tdd.money.jsonholder.ExchangeRateApiPairResponse;
import com.thesoftwaregorilla.tdd.money.jsonholder.ExchangeRateApiStandardResponse;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import com.fasterxml.jackson.databind.ObjectMapper;

import static com.thesoftwaregorilla.tdd.money.AppConfigTest.API_PAIR_URL;
import static com.thesoftwaregorilla.tdd.money.AppConfigTest.API_STANDARD_URL;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO's">
//TODO: Refactor tests to segregate each class into its own test class
//TODO: Add an CurrencyExchange object to cache all exchange rates and lazy load them as needed.
//TODO: Get to 100% code coverage.
//TODO: Create the utility class for the JSON parsing.
//TODO: Create a utility class for the HTTP requests.
//TODO: Build solution for error responses.
//</editor-fold>

public class ExchangeRateAPITest {

    private static final String BASE_CURRENCY = "USD";
    private static final String TARGET_CURRENCY = "ZAR";
    private static final String ERROR_NO_KEY =
            """
            {"result":"error","error-type":"invalid-key","message":"Invalid API key"}""";
    private static final String ERROR_INVALID_KEY =
            """
            {"result":"error","documentation":"https://www.exchangerate-api.com/docs","terms-of-use":"https://www.exchangerate-api.com/terms","error-type":"invalid-key"}""";
    private static final String STANDARD_RESPONSE_FILE = "src/test/resources/exchange_api_standard_response.json";
    private static final String PAIR_RESPONSE_FILE = "src/test/resources/exchange_api_pair_response.json";

    private static final String API_KEY_PROPERTY;
    private static final String API_PAIR_URL_PROPERTY;
    private static final String API_STANDARD_URL_PROPERTY;
    private static final AppConfig APP_CONFIG;
    static {
        APP_CONFIG = new AppConfig("src/main/resources/application.settings");
        API_KEY_PROPERTY = APP_CONFIG.getApiKey();
        API_PAIR_URL_PROPERTY = APP_CONFIG.getExchangeApiPairUrl();
        API_STANDARD_URL_PROPERTY = APP_CONFIG.getExchangeApiStandardUrl();
    }

    @Test
    public void testHttpGetStandard() {
        GetRequest getRequest = new GetRequest(String.format(API_STANDARD_URL_PROPERTY, API_KEY_PROPERTY, BASE_CURRENCY));
        assertNotNull(getRequest);
        assertEquals(String.format(API_STANDARD_URL, API_KEY_PROPERTY, BASE_CURRENCY), getRequest.getUrl());
        String response = getRequest.fetchResponse();
        assertNotNull(response);
        assertNotEquals(ERROR_INVALID_KEY, response);
        assertNotEquals(ERROR_NO_KEY, response);
//        writeResponseToFile(response, "src/test/resources/exchange_api_standard_response.jsonholder");
    }

    @Test
    public void testHttpGetPair() {
        GetRequest getRequest = new GetRequest(String.format(API_PAIR_URL_PROPERTY, API_KEY_PROPERTY, BASE_CURRENCY, TARGET_CURRENCY));
        assertNotNull(getRequest);
        assertEquals(String.format(API_PAIR_URL, API_KEY_PROPERTY, BASE_CURRENCY, TARGET_CURRENCY), getRequest.getUrl());
        String response = getRequest.fetchResponse();
        assertNotNull(response);
        assertNotEquals(ERROR_INVALID_KEY, response);
        assertNotEquals(ERROR_NO_KEY, response);
//        writeResponseToFile(response, "src/test/resources/exchange_api_pair_response.jsonholder");
    }

    @Test
    public void parsePairResponse() {
        ExchangeRateApiPairResponse response = parseResponse(new File(PAIR_RESPONSE_FILE), ExchangeRateApiPairResponse.class);
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        assertEquals("ZAR", response.getTarget_code());
        assertTrue(response.getConversion_rate() > 0);
    }

    @Test
    public void parseStandardResponse() {
        ExchangeRateApiStandardResponse response = parseResponse(new File(STANDARD_RESPONSE_FILE), ExchangeRateApiStandardResponse.class);
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        assertNotNull(response.getConversion_rates());
        assertFalse(response.getConversion_rates().isEmpty());
    }

    @Test
    public void testExchangeApiFetcher() {
        ExchangeRateApiFetcher fetcher = new ExchangeRateApiFetcher(APP_CONFIG, BASE_CURRENCY).fetch();
        assertNotNull(fetcher);
        assertEquals(BASE_CURRENCY, fetcher.getBaseCurrency());
        assertNotNull(fetcher.getTimeLastUpdateUtc());
        assertTrue(fetcher.isRateExists(TARGET_CURRENCY));
        assertTrue(fetcher.getConversionRate(TARGET_CURRENCY) > 0);
    }

    private <T> T parseResponse(File response, Class<T> responseType) {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(response, responseType);
        } catch (IOException e) {
            e.printStackTrace();
            fail("Failed to parse response");
            return null;
        }
    }

    private <T> T parseResponse(String response, Class<T> responseType) {
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(response, responseType);
        } catch (IOException e) {
            e.printStackTrace();
            fail("Failed to parse response");
            return null;
        }
    }

    private void writeResponseToFile(String response, String filePath) {
        try {
            Path path = Paths.get(filePath);
            Files.write(path, response.getBytes());
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
}
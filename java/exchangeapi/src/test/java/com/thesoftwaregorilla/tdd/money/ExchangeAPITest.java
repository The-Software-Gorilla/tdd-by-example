package com.thesoftwaregorilla.tdd.money;

import com.thesoftwaregorilla.tdd.money.http.GetRequest;
import com.thesoftwaregorilla.tdd.money.json.ExchangeApiPairResponse;
import com.thesoftwaregorilla.tdd.money.json.ExchangeApiStandardResponse;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import com.fasterxml.jackson.databind.ObjectMapper;

import static com.thesoftwaregorilla.tdd.money.AppConfigTest.EXCHANGE_API_PAIR_URL;
import static com.thesoftwaregorilla.tdd.money.AppConfigTest.EXCHANGE_API_STANDARD_URL;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO's">
//TODO: Refactor tests to segregate each class into its own test class
//TODO: Add an Exchange object to cache all exchange rates and lazy load them as needed.
//TODO: Get to 100% code coverage.
//TODO: Create the utility class for the JSON parsing.
//TODO: Create a utility class for the HTTP requests.
//TODO: Build solution for error responses.
//</editor-fold>

public class ExchangeAPITest {

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
        assertEquals(String.format(EXCHANGE_API_STANDARD_URL, API_KEY_PROPERTY, BASE_CURRENCY), getRequest.getUrl());
        String response = getRequest.fetchResponse();
        assertNotNull(response);
        assertNotEquals(ERROR_INVALID_KEY, response);
        assertNotEquals(ERROR_NO_KEY, response);
//        writeResponseToFile(response, "src/test/resources/exchange_api_standard_response.json");
    }

    @Test
    public void testHttpGetPair() {
        GetRequest getRequest = new GetRequest(String.format(API_PAIR_URL_PROPERTY, API_KEY_PROPERTY, BASE_CURRENCY, TARGET_CURRENCY));
        assertNotNull(getRequest);
        assertEquals(String.format(EXCHANGE_API_PAIR_URL, API_KEY_PROPERTY, BASE_CURRENCY, TARGET_CURRENCY), getRequest.getUrl());
        String response = getRequest.fetchResponse();
        assertNotNull(response);
        assertNotEquals(ERROR_INVALID_KEY, response);
        assertNotEquals(ERROR_NO_KEY, response);
//        writeResponseToFile(response, "src/test/resources/exchange_api_pair_response.json");
    }

    @Test
    public void parsePairResponse() {
        ExchangeApiPairResponse response = parseResponse(new File(PAIR_RESPONSE_FILE), ExchangeApiPairResponse.class);
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        assertEquals("ZAR", response.getTarget_code());
        assertTrue(response.getConversion_rate() > 0);
        System.out.println("%s to %s conversion rate: %f".formatted(response.getBase_code(), response.getTarget_code(), response.getConversion_rate()));
    }

    @Test
    public void parseStandardResponse() {
        ExchangeApiStandardResponse response = parseResponse(new File(STANDARD_RESPONSE_FILE), ExchangeApiStandardResponse.class);
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        assertNotNull(response.getConversion_rates());
        assertFalse(response.getConversion_rates().isEmpty());
        System.out.println("Conversion rates: " + response.getConversion_rates());
    }

    @Test
    public void testExchangeApiFetcher() {
        ExchangeApiFetcher fetcher = new ExchangeApiFetcher(APP_CONFIG, BASE_CURRENCY).fetch();
        assertNotNull(fetcher);
        assertEquals(BASE_CURRENCY, fetcher.getBaseCurrency());
        assertNotNull(fetcher.getTimeLastUpdateUtc());
        assertTrue(fetcher.isRateExists(TARGET_CURRENCY));
        assertTrue(fetcher.getConversionRate(TARGET_CURRENCY) > 0);
        System.out.println(fetcher.toString());
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
        // Implement parsing logic here
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
        // Implement parsing logic here
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
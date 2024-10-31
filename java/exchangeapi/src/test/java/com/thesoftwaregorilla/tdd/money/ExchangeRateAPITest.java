package com.thesoftwaregorilla.tdd.money;

import com.thesoftwaregorilla.helper.DateHelper;
import com.thesoftwaregorilla.helper.JsonHelper;
import com.thesoftwaregorilla.helper.StandardDateFormat;
import com.thesoftwaregorilla.tdd.money.http.GetRequest;
import com.thesoftwaregorilla.tdd.money.jsonholder.ExchangeRateApiPairResponse;
import com.thesoftwaregorilla.tdd.money.jsonholder.ExchangeRateApiStandardResponse;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.text.ParseException;
import java.util.Date;

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
//        try{
//            FileHelper.writeStringToFile(response, STANDARD_RESPONSE_FILE);
//        } catch (IOException e) {
//            fail(e);
//        }
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
//        try {
//            FileHelper.writeStringToFile(response, PAIR_RESPONSE_FILE);
//        } catch (IOException e) {
//            fail(e);
//        }
    }

    @Test
    public void parsePairResponse() {
        ExchangeRateApiPairResponse response = null;
        try {
            response = JsonHelper.parseResponse(new File(PAIR_RESPONSE_FILE), ExchangeRateApiPairResponse.class);
        } catch (IOException e) {
            fail(e);
        }
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        assertEquals("ZAR", response.getTarget_code());
        assertTrue(response.getConversion_rate() > 0);
    }

    @Test
    public void parseStandardResponse() {
        ExchangeRateApiStandardResponse response = null;
        try {
            response = JsonHelper.parseResponse(new File(STANDARD_RESPONSE_FILE), ExchangeRateApiStandardResponse.class);
        } catch (IOException e) {
           fail(e);
        }
        assertNotNull(response);
        assertEquals("USD", response.getBase_code());
        Date unixDate = DateHelper.fromUnixTimestamp(response.getTime_last_update_unix());
        Date date = null;
        try {
            date = DateHelper.fromStringUtc(response.getTime_last_update_utc(), StandardDateFormat.RFC1123.getDateFormat());
        } catch (ParseException e) {
            fail(String.format("Failed to parse date: %s", response.getTime_last_update_utc()));
        }
        assertEquals(date, unixDate);
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



}
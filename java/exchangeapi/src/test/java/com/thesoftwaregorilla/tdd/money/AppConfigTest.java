package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

public class AppConfigTest {
    public static final String EXCHANGE_API_PAIR_URL = "https://v6.exchangerate-api.com/v6/%s/pair/%s/%s";
    public static final String EXCHANGE_API_STANDARD_URL = "https://v6.exchangerate-api.com/v6/%s/latest/%s";


    @Test
    public void testAppConfig() {
        AppConfig config = new AppConfig("src/main/resources/application.settings");
        assertNotNull(config);
        assertNotNull(config.getApiKey());
        assertEquals(EXCHANGE_API_PAIR_URL, config.getExchangeApiPairUrl());
        assertEquals(EXCHANGE_API_STANDARD_URL, config.getExchangeApiStandardUrl());
    }
}

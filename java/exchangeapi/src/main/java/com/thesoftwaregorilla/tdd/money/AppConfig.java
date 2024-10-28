package com.thesoftwaregorilla.tdd.money;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;
import java.util.logging.Logger;

public class AppConfig {

    private final Logger logger = Logger.getLogger(AppConfig.class.getName());

    private final Properties properties = new Properties();
    private final Properties apiKeys = new Properties();
    public AppConfig(String configFilePath) {
        try (FileInputStream input = new FileInputStream(configFilePath)) {
            properties.load(input);
            apiKeys.load(new FileInputStream(properties.getProperty("api.key.location")));
        } catch (IOException e) {
            logger.severe("Could not load configuration file: " + e.getMessage());
        }
    }

    public String getApiKey() {
        return apiKeys.getProperty(properties.getProperty("api.key.name"));
    }

    public String getExchangeApiPairUrl() {
        return properties.getProperty("exchange.api.pair.url");
    }

    public String getExchangeApiStandardUrl() {
        return properties.getProperty("exchange.api.standard.url");
    }
}

package com.thesoftwaregorilla.tdd.money.helper;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.File;
import java.io.IOException;

public class JsonHelper {
    public static <T> T parseResponse(File response, Class<T> responseType) throws IOException {
        ObjectMapper objectMapper = new ObjectMapper();
        return objectMapper.readValue(response, responseType);
    }

    public static <T> T parseResponse(String response, Class<T> responseType) throws JsonProcessingException {
        ObjectMapper objectMapper = new ObjectMapper();
        return objectMapper.readValue(response, responseType);
    }

}

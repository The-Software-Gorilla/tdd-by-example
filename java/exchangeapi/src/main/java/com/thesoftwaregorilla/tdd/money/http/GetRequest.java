package com.thesoftwaregorilla.tdd.money.http;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.util.logging.Level;
import java.util.logging.Logger;

public class GetRequest {

    private final Logger logger = Logger.getLogger(GetRequest.class.getName());

    public String url;
    public GetRequest(String url) {
        this.url = url;
    }

    public String fetchResponse() {
        try (HttpClient client = HttpClient.newHttpClient()){
            HttpRequest request = HttpRequest.newBuilder()
                    .uri(new URI(url))
                    .GET()
                    .build();

            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

            return response.body();
        } catch (Exception e) {
            logger.log(Level.SEVERE, "Error fetching response: " + e.getMessage());
            return null;
        }
    }

    public String getUrl() {
        return url;
    }
}

package com.thesoftwaregorilla.tdd.money.helper;

import java.text.SimpleDateFormat;

public enum StandardDateFormat {
    ISO("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    SIMPLE("yyyy-MM-dd"),
    SIMPLE_US("MM/dd/yyyy"),
    SIMPLE_EU("dd/MM/yyyy"),
    ISO8601("yyyy-MM-dd'T'HH:mm:ss.SSSXXX"),
    RFC1123("EEE, dd MMM yyyy HH:mm:ss zzz");

    private final String format;

    StandardDateFormat(String format) {
        this.format = format;
    }

    public String getDateFormatString() {
        return format;
    }

    public SimpleDateFormat getDateFormat() {
        return new SimpleDateFormat(format);
    }
}

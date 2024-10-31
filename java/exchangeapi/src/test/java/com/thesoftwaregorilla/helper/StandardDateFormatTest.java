package com.thesoftwaregorilla.helper;

import org.junit.jupiter.api.Test;

import java.text.SimpleDateFormat;
import java.util.TimeZone;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class StandardDateFormatTest {
    @Test
    void testISOPattern() {
        SimpleDateFormat sdf = StandardDateFormat.ISO.getDateFormat();
        assertEquals("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", sdf.toPattern());
    }

    @Test
    void testSimplePattern() {
        SimpleDateFormat sdf = StandardDateFormat.SIMPLE.getDateFormat();
        assertEquals("yyyy-MM-dd", sdf.toPattern());
    }

    @Test
    void testSimpleUSPattern() {
        SimpleDateFormat sdf = StandardDateFormat.SIMPLE_US.getDateFormat();
        assertEquals("MM/dd/yyyy", sdf.toPattern());
    }

    @Test
    void testSimpleEUPattern() {
        SimpleDateFormat sdf = StandardDateFormat.SIMPLE_EU.getDateFormat();
        assertEquals("dd/MM/yyyy", sdf.toPattern());
    }

    @Test
    void testISO8601Pattern() {
        SimpleDateFormat sdf = StandardDateFormat.ISO8601.getDateFormat();
        assertEquals("yyyy-MM-dd'T'HH:mm:ss.SSSXXX", sdf.toPattern());
    }

    @Test
    void testRFC1123Pattern() {
        SimpleDateFormat sdf = StandardDateFormat.RFC1123.getDateFormat();
        assertEquals("EEE, dd MMM yyyy HH:mm:ss zzz", sdf.toPattern());
    }

    @Test
    void testTimeZone() {
        for (StandardDateFormat sdf : StandardDateFormat.values()) {
            assertEquals(TimeZone.getDefault(), sdf.getDateFormat().getTimeZone());
        }
    }
}
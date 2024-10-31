package com.thesoftwaregorilla.helper;

import org.junit.jupiter.api.Test;

import java.text.SimpleDateFormat;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.assertEquals;

class DateHelperTest {

    @Test
    void getFormattedDate_WithNullDate() {
        String result = DateHelper.getFormattedDate(null);
        assertEquals("null", result);
    }

    @Test
    void getFormattedDate_WithStringFormat() {
        SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss.SSS");
        String dateStr = "20/12/2020 12:00:00.000";
        Date date;
        try {
            date = sdf.parse(dateStr);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
        String result = DateHelper.getFormattedDate(date, "dd/MM/yyyy HH:mm:ss.SSS");
        assertEquals(dateStr, result);
    }

    @Test
    void getFormattedDate_WithoutStringFormat() {
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSX");
        String dateStr = "2020-12-20T12:00:00.000-08:00";
        Date date;
        try {
            date = sdf.parse(dateStr);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
        String result = DateHelper.getFormattedDate(date);
        assertEquals(dateStr, result);
    }
}
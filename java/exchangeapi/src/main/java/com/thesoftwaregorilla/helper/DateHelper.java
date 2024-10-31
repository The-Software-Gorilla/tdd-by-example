package com.thesoftwaregorilla.helper;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.Date;
import java.util.TimeZone;

public class DateHelper {

    public static final Date MAX_VALUE = new Date(Long.MAX_VALUE);
    public static final Date MIN_VALUE = new Date(-90000000000000L);

    public static String getFormattedDate(Date date) {
        return getFormattedDate(date, null);
    }

    public static String getFormattedDate(Date date, String format) {
        if (date == null) {
            return "null";
        }
        DateFormat fmt;
        if (format == null || format.isEmpty()) {
            fmt = StandardDateFormat.ISO8601.getDateFormat();
        }
        else {
            fmt = new SimpleDateFormat(format);
        }
        return fmt.format(date);
    }

    public static Date fromUnixTimestamp(long timestamp) {
        Instant instant = Instant.ofEpochSecond(timestamp);
        ZonedDateTime zdt = instant.atZone(ZoneId.of("UTC"));
        return Date.from(zdt.toInstant());
    }

    public static Date fromStringUtc(String dateString) throws ParseException {
            return fromStringUtc(dateString, StandardDateFormat.RFC1123.getDateFormat());
    }

    public static Date fromStringUtc(String dateString, DateFormat dateFormat) throws ParseException {
        dateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
        Date date = dateFormat.parse(dateString);
        dateFormat.setTimeZone(TimeZone.getDefault());
        return dateFormat.parse(dateFormat.format(date));
    }

}

package com.thesoftwaregorilla.tdd.money;


import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.HashMap;

class Bank {

    private final HashMap<CurrencyPair, BigDecimal> rates = new HashMap<>();

    Money reduce(Expression source, String to) {
        return source.reduce(this, to);
    }

    BigDecimal rate(String from, String to) {
        if (from.equals(to)) {
            return BigDecimal.valueOf(1L);
        }
        BigDecimal rate = rates.get(new CurrencyPair(from, to));
        return rate != null ? rate : BigDecimal.valueOf(0L);
    }

    void addRate(String from, String to, BigDecimal rate) {
        rates.put(new CurrencyPair(from, to), rate);
    }

}

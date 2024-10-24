package com.thesoftwaregorilla.tdd.money;


import java.util.HashMap;

public class Bank {

    private final HashMap<CurrencyPair, Integer> rates = new HashMap<>();

    public Money reduce(Expression source, String to) {
        return source.reduce(this, to);
    }

    public int rate(String from, String to) {
        if (from.equals(to)) {
            return 1;
        }
        Integer rate = rates.get(new CurrencyPair(from, to));
        return rate != null ? rate : 0;
    }

    public void addRate(String from, String to, int rate) {
        rates.put(new CurrencyPair(from, to), rate);
    }

}

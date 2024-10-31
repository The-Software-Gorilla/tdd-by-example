package com.thesoftwaregorilla.tdd.money;


import java.util.HashMap;

class Bank {

    private final HashMap<CurrencyPair, Integer> rates = new HashMap<>();

    Money reduce(Expression source, String to) {
        return source.reduce(this, to);
    }

    int rate(String from, String to) {
        if (from.equals(to)) {
            return 1;
        }
        Integer rate = rates.get(new CurrencyPair(from, to));
        return rate != null ? rate : 0;
    }

    void addRate(String from, String to, int rate) {
        rates.put(new CurrencyPair(from, to), rate);
    }

}

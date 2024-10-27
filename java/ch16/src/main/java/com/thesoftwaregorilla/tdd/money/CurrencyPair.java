package com.thesoftwaregorilla.tdd.money;

import java.util.Objects;

public class CurrencyPair {
    private final String from;
    private final String to;

    public CurrencyPair(String from, String to) {
        this.from = from;
        this.to = to;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (!(obj instanceof CurrencyPair)) return false;
        CurrencyPair pair = (CurrencyPair) obj;
        return from.equals(pair.from) && to.equals(pair.to);
    }

    // The book uses zero for a hashCode. Objects.hash is a better way to do this, but was only added to Java 7.
    @Override
    public int hashCode() {
        return Objects.hash(from, to);
    }
}
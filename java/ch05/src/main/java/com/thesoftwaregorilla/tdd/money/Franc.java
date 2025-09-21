package com.thesoftwaregorilla.tdd.money;

public class Franc {
    private final int amount;

    Franc(int amount) {
        this.amount = amount;
    }

    public Franc times(int multiplier) {
        return new Franc(amount * multiplier);
    }

    public boolean equals(Object object) {
        return object instanceof Franc franc && this.amount == franc.amount;
    }

    public int getAmount() {
        return amount;
    }
}

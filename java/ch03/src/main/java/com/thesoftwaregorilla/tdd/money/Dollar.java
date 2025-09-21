package com.thesoftwaregorilla.tdd.money;

public class Dollar {
    int amount;

    Dollar(int amount) {
        this.amount = amount;
    }

    public Dollar times(int multiplier) {
        return new Dollar(amount * multiplier);
    }

    public boolean equals(Object object) {
        return object instanceof Dollar dollar && this.amount == dollar.amount;
    }
}

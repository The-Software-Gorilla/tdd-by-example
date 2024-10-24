package com.thesoftwaregorilla.tdd.money;

public class Dollar {
    private final int amount;

    public Dollar(int amount) {
        this.amount = amount;
    }

    public Dollar times(int multiplier) {
        return new Dollar(amount * multiplier);
    }

    public boolean equals(Object object) {
        Dollar dollar = (Dollar) object;
        return amount == dollar.amount;
    }

    // See my note in the DollarTest class. I added a getter for amount because I had a constructor test.
    public int getAmount() {
        return amount;
    }
}

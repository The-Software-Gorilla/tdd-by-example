package com.thesoftwaregorilla.tdd.money;

public class Money {
    protected int amount;

    public boolean equals(Object object) {
        Money money = (Money) object;
        return amount == money.amount;
    }

    // See my note in the DollarTest class. I added a getter for amount because I had a constructor test.
    public int getAmount() {
        return amount;
    }

}

package com.thesoftwaregorilla.tdd.money;

public class Money {
    protected int amount;

    public boolean equals(Object object) {
        return object instanceof Money money && getClass().equals(object.getClass()) && this.amount == money.amount;
    }

    // See my note in the DollarTest class. I added a getter for amount because I had a constructor test.
    public int getAmount() {
        return amount;
    }

}

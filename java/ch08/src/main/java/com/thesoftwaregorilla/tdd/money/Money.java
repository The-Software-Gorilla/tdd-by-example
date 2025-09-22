package com.thesoftwaregorilla.tdd.money;

public abstract class Money {
    protected int amount;

    public static Money dollar(int amount) {
        return new Dollar(amount);
    }

    public static Money franc(int amount) {
        return new Franc(amount);
    }

    public abstract Money times(int multiplier);

    @Override
    public boolean equals(Object object) {
        return object instanceof Money money && getClass().equals(object.getClass()) && this.amount == money.amount;
    }

    // See my note in the DollarTest class. I added a getter for amount because I had a constructor test.
    public int getAmount() {
        return amount;
    }

}

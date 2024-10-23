package com.thesoftwaregorilla.tdd.money;

public abstract class Money {

    public static Money dollar(int amount) {
        return new Dollar(amount, "USD");
    }

    public static Money franc(int amount) {
        return new Franc(amount, "CHF");
    }


    private final int amount;
    private final String currency;

    protected Money(int amount, String currency) {
        this.amount = amount;
        this.currency = currency;
    }

    public abstract Money times(int multiplier);

    @Override
    public boolean equals(Object object) {
        Money money = (Money) object;
        return getClass().equals(object.getClass()) && amount == money.amount;
    }

    // See my note in the MoneyTest class. I added a getter for amount because I had a constructor test.
    public int getAmount() {
        return amount;
    }

    // I changed the name of this method from "currency" to "getCurrency" to keep consistent with Java standards.
    // I'm not sure these standards existed when Kent wrote the book.
    public String getCurrency() {
        return currency;
    }

}

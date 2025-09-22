package com.thesoftwaregorilla.tdd.money;


public class Money implements Expression {

    public static Money dollar(int amount) {
        return new Money(amount, "USD");
    }

    public static Money franc(int amount) {
        return new Money(amount, "CHF");
    }

    // Shout out to all my South African friends!
    public static Money rand(int amount) {
        return new Money(amount, "ZAR");
    }

    private final int amount;
    private final String currency;

    private Money(int amount, String currency) {
        this.amount = amount;
        this.currency = currency;
    }

    public Money times(int multiplier) {
        return new Money(getAmount() * multiplier, getCurrency());
    };

    public Expression plus(Money addend) {
        return new Money(getAmount() + addend.getAmount(), getCurrency());
    }

    @Override
    public boolean equals(Object object) {
        return object instanceof Money money && this.amount == money.amount && this.currency.equals(money.currency);
    }

    @Override
    public String toString() {
        return amount + " " + currency;
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

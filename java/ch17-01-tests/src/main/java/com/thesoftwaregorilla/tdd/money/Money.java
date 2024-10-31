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

    Money(int amount, String currency) {
        this.amount = amount;
        this.currency = currency;
    }

    public Expression times(int multiplier) {
        return new Money(getAmount() * multiplier, getCurrency());
    };

    public Expression plus(Expression addend) {
        return new Sum(this, addend);
    }

    public Money reduce(Bank bank, String to) {
        int rate = bank.rate(getCurrency(), to);
        if (rate == 0) {
            throw new ArithmeticException("Exchange rate not available");
        }
        return new Money(getAmount() / rate, to);
    }

    @Override
    public boolean equals(Object object) {
        Money money = (Money) object;
        return getAmount() == money.getAmount() && getCurrency().equals(money.getCurrency());
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

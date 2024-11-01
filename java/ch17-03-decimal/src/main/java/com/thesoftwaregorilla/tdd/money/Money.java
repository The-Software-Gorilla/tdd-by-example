package com.thesoftwaregorilla.tdd.money;


import java.math.BigDecimal;
import java.math.RoundingMode;

public class Money extends Expression {

    public static Money dollar(BigDecimal amount) {
        return new Money(amount, "USD");
    }

    public static Money franc(BigDecimal amount) {
        return new Money(amount, "CHF");
    }

    // Shout out to all my South African friends!
    public static Money rand(BigDecimal amount) {
        return new Money(amount, "ZAR");
    }


    private final BigDecimal amount;
    private final String currency;

    Money(BigDecimal amount, String currency) {
        this.amount = amount.setScale(2, RoundingMode.HALF_UP);
        this.currency = currency;
    }

    @Override
    public Expression times(BigDecimal multiplier) {
        return new Money(getAmount().multiply(multiplier), getCurrency());
    };

    public Expression plus(Expression addend) {
        if (addend instanceof Money money && this.currency.equals(money.getCurrency())) {
            return new Money(getAmount().add(money.getAmount()), getCurrency());
        }
        return super.plus(addend);
    }

    @Override
    public Money reduce(Bank bank, String to) {
        BigDecimal rate = bank.rate(getCurrency(), to);
        if (rate.equals(BigDecimal.ZERO.setScale(8, RoundingMode.HALF_UP))) {
            throw new ArithmeticException("Exchange rate not available");
        }
        return new Money(getAmount().divide(rate, RoundingMode.HALF_UP), to);
    }

    @Override
    public boolean equals(Object object) {
        Money money = (Money) object;
        return getAmount().equals(money.getAmount()) && getCurrency().equals(money.getCurrency());
    }

    @Override
    public String toString() {
        return amount + " " + currency;
    }

    // See my note in the MoneyTest class. I added a getter for amount because I had a constructor test.
    public BigDecimal getAmount() {
        return amount;
    }

    // I changed the name of this method from "currency" to "getCurrency" to keep consistent with Java standards.
    // I'm not sure these standards existed when Kent wrote the book.
    public String getCurrency() {
        return currency;
    }

}

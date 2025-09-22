package com.thesoftwaregorilla.tdd.money;

public class Franc extends Money {


    Franc(int amount, String currency) {
        super(amount, currency);
    }


    public Money times(int multiplier) {
        return Money.franc(getAmount() * multiplier);
    }

}

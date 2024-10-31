package com.thesoftwaregorilla.tdd.money;

public abstract class Expression {
    public abstract Money reduce(Bank bank, String to);
    public Expression plus(Expression addend) {
        return new Sum(this, addend);
    }
    public abstract Expression times(int multiplier);
}


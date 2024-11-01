package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;

public abstract class Expression {
    public abstract Money reduce(Bank bank, String to);
    public Expression plus(Expression addend) {
        return new Sum(this, addend);
    }
    public abstract Expression times(BigDecimal multiplier);
}


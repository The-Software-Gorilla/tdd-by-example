package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;

class Sum extends Expression {
    final Expression augend;
    final Expression addend;

    Sum(Expression augend, Expression addend) {
        this.augend = augend;
        this.addend = addend;
    }

    @Override
    public Money reduce(Bank bank, String to) {
        return new Money(augend.reduce(bank, to).getAmount().add(addend.reduce(bank, to).getAmount()), to);
    }

    @Override
    public Expression times(BigDecimal multiplier) {
        return new Sum(augend.times(multiplier), addend.times(multiplier));
    }
}

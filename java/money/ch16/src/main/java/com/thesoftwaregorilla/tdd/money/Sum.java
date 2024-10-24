package com.thesoftwaregorilla.tdd.money;

public class Sum implements Expression {
    final Expression augend;
    final Expression addend;

    public Sum(Expression augend, Expression addend) {
        this.augend = augend;
        this.addend = addend;
    }

    public Money reduce(Bank bank, String to) {
        return new Money(augend.reduce(bank, to).getAmount() + addend.reduce(bank, to).getAmount(), to);
    }

    public Expression plus(Expression addend) {
        return null;
    }
}

package com.thesoftwaregorilla.tdd.money;

class Sum extends Expression {
    final Expression augend;
    final Expression addend;

    Sum(Expression augend, Expression addend) {
        this.augend = augend;
        this.addend = addend;
    }

    @Override
    public Money reduce(Bank bank, String to) {
        return new Money(augend.reduce(bank, to).getAmount() + addend.reduce(bank, to).getAmount(), to);
    }

    @Override
    public Expression times(int multiplier) {
        return new Sum(augend.times(multiplier), addend.times(multiplier));
    }
}

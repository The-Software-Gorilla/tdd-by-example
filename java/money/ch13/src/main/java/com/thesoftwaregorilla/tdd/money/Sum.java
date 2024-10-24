package com.thesoftwaregorilla.tdd.money;

public class Sum implements Expression {
    final Money augend;
    final Money addend;

    public Sum(Money augend, Money addend) {
        this.augend = augend;
        this.addend = addend;
    }

    public Money reduce(String to) {
        return new Money(augend.getAmount() + addend.getAmount(), to);
    }
}

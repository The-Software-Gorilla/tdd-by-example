package com.thesoftwaregorilla.tdd.money;

import java.util.function.BiFunction;

public class Operation<T extends ICurrencyHolder<T>> implements IOperation<T> {

    private final T leftOperand;
    private final T rightOperand;
    private final Bank<T> bank;
    private final String toCurrency;
    private final BiFunction<T, T, T> calculation;

    public Operation(T leftOperand, T rightOperand, Bank<T> bank, String toCurrency, BiFunction<T, T, T> calculation) {
        this.leftOperand = leftOperand;
        this.rightOperand = rightOperand;
        this.bank = bank;
        this.calculation = calculation;
        this.toCurrency = toCurrency;
    }

    public T evaluate() {
        T left = leftOperand.convert(toCurrency);
        T right = rightOperand.convert(toCurrency);
        return calculation.apply(left, right);
    }

    public T getLeftOperand() {
        return leftOperand;
    }

    public T getRightOperand() {
        return rightOperand;
    }

    public Bank<T> getBank() {
        return bank;
    }

}

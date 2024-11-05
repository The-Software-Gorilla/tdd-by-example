package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;

public interface IExpression<T extends ICurrencyHolder<T>> {
    public T add(T addend);
    public T subtract(T subtrahend);
    public T multiply(BigDecimal multiplier);
    public T divide(BigDecimal divisor);
}

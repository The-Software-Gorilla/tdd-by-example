package com.thesoftwaregorilla.tdd.money;

public interface IOperation<T extends ICurrencyHolder<T>>  {
    public T evaluate();
}

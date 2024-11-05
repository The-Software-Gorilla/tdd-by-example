package com.thesoftwaregorilla.tdd.money;

public interface ICurrencyConverter<T extends ICurrencyHolder<T>>  {
    public T convert(T holder, String toCurrency);
}

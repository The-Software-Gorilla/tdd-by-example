package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;

public interface ICurrencyHolder<T extends ICurrencyHolder<T>> {

    public Bank<T> getBank();
    public String getCurrency();
    public BigDecimal getAmount();
    public T convert(String toCurrency);
    public BigDecimal getAmountIn(String toCurrency);
    public T newCurrencyHolder(BigDecimal amount, String currency, Bank<T> bank);

}

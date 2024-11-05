package com.thesoftwaregorilla.tdd.money;


import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.HashMap;

public class Bank<T extends ICurrencyHolder<T>> implements ICurrencyConverter<T> {

    private final HashMap<CurrencyPair, BigDecimal> rates = new HashMap<>();

    @Override
    public T convert(T holder, String toCurrency) {
        BigDecimal rate = this.rate(holder.getCurrency(), toCurrency);
        if (rate.equals(BigDecimal.ZERO.setScale(8, RoundingMode.HALF_UP))) {
            throw new ArithmeticException("Exchange rate not available");
        }
        return holder.newCurrencyHolder(holder.getAmount().multiply(rate), toCurrency, this);
    }


    public BigDecimal rate(String from, String to) {
        if (from.equals(to)) {
            return BigDecimal.valueOf(1L).setScale(8, RoundingMode.HALF_UP);
        }
        BigDecimal rate = rates.get(new CurrencyPair(from, to));
        return rate != null ? rate : BigDecimal.valueOf(0L).setScale(8, RoundingMode.HALF_UP);
    }

    public void addRate(String from, String to, BigDecimal rate) {
        rates.put(new CurrencyPair(from, to), rate.setScale(8, RoundingMode.HALF_UP));
    }

}

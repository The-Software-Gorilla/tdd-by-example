package com.thesoftwaregorilla.tdd.money;


import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.function.BiFunction;


public class Money implements ICurrencyHolder<Money>, IExpression<Money> {


    public static Money from(BigDecimal amount, String currency, Bank<Money> bank) {
        return new Money(amount, currency, bank);
    }

    private final BigDecimal amount;
    private final String currency;
    private final Bank<Money> bank;

    protected Money(BigDecimal amount, String currency, Bank<Money> bank) {
        this.amount = amount.setScale(2, RoundingMode.HALF_UP);
        this.currency = currency;
        this.bank = bank;
    }


    @Override
    public Bank<Money> getBank() {
        return bank;
    }

    @Override
    public Money convert(String toCurrency) {
        return bank.convert(this, toCurrency);
    }

    @Override
    public BigDecimal getAmount() {
        return amount;
    }

    @Override
    public BigDecimal getAmountIn(String toCurrency) {
        return convert(toCurrency).getAmount();
    }

    @Override
    public String getCurrency() {
        return currency;
    }

    @Override
    public Money newCurrencyHolder(BigDecimal amount, String currency, Bank<Money> bank) {
        return Money.from(amount, currency, bank);
    }

    @Override
    public Money add(Money addend) {
        BiFunction<Money, Money, Money> operation = (a, b) -> Money.from(a.getAmount().add(b.getAmount()), a.getCurrency(), a.getBank());
        return InvokeOperator(addend, operation);
    }

    @Override
    public Money subtract(Money subtrahend) {
        BiFunction<Money, Money, Money> operation = (a, b) -> Money.from(a.getAmount().subtract(b.getAmount()), a.getCurrency(), a.getBank());
        return InvokeOperator(subtrahend, operation);
    }

    @Override
    public Money multiply(BigDecimal multiplier) {
        return Money.from(getAmount().multiply(multiplier), getCurrency(), getBank());
    }

    @Override
    public Money divide(BigDecimal divisor) {
        return Money.from(getAmount().divide(divisor, 2, RoundingMode.HALF_UP), getCurrency(), getBank());
    }

    protected Money InvokeOperator(Money other, BiFunction<Money, Money, Money> operation) {
        Operation<Money> operationInstance = new Operation<>(this, other, bank, currency, operation);
        return operationInstance.evaluate();
    }

    @Override
    public boolean equals(Object object) {
        return object instanceof ICurrencyHolder<?> money
                && getAmount().equals(money.getAmount())
                && getCurrency().equals(money.getCurrency());
    }

    @Override
    public int hashCode() {
        return getAmount().hashCode() + getCurrency().hashCode() + getBank().hashCode();
    }

    @Override
    public String toString() {
        return amount + " " + currency;
    }
}

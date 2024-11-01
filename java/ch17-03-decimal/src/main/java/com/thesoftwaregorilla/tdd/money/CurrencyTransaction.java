package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class CurrencyTransaction {
    private final Bank bank;
    private final Money sourceAmount;
    private final String targetCurrency;

    private Money sourceFee;
    private BigDecimal targetCurrencyRateFeePerc;
    private BigDecimal targetConversionRate;
    private Money targetAmountAfterRateFee;
    private Money targetCurrencyFee;


    private Money targetServiceFee;
    private Money totalTargetFees;
    private Money totalTransactionFees;
    private Money totalTransactionAmount;
    private Money settlementAmount;

    public CurrencyTransaction(Bank bank,Money sourceAmount, String targetCurrency) {
        this.bank = bank;
        this.sourceAmount = sourceAmount;
        this.targetCurrency = targetCurrency;
    }

    public void setSourceFee(Money fee) {
        this.sourceFee = fee;
    }

    public void setTargetCurrencyRateFeePerc(BigDecimal rate) {
        this.targetCurrencyRateFeePerc = rate;
    }

    public void setTargetServiceFee(Money fee) {
        this.targetServiceFee = fee;
    }

    public void settle() {
        BigDecimal bankRate = bank.rate(sourceAmount.getCurrency(),targetCurrency);
        BigDecimal rateFee = bankRate.multiply(targetCurrencyRateFeePerc).setScale(8, RoundingMode.HALF_UP);
        this.targetConversionRate = bankRate.subtract(rateFee).setScale(8, RoundingMode.HALF_UP);
        this.targetAmountAfterRateFee = new Money(sourceAmount.getAmount().multiply(targetConversionRate), targetCurrency);
        Money srcInDest = sourceAmount.reduce(bank, targetCurrency);
        this.targetCurrencyFee = new Money(srcInDest.getAmount().subtract(targetAmountAfterRateFee.getAmount()), targetCurrency);
        this.totalTargetFees = targetCurrencyFee.plus(targetServiceFee).reduce(bank, targetCurrency);
        this.totalTransactionFees = sourceFee.plus(totalTargetFees).reduce(bank, targetCurrency);
        this.totalTransactionAmount = sourceAmount.plus(this.sourceFee).reduce(bank, sourceAmount.getCurrency());
        this.settlementAmount = new Money(this.targetAmountAfterRateFee.getAmount().subtract(targetServiceFee.getAmount()), targetCurrency);
    }

    public BigDecimal getTargetConversionRate() {
        return targetConversionRate;
    }

    public Money getSettlementAmount(String currency) {
        return settlementAmount.reduce(bank, currency);
    }

    public Money getTargetCurrencyFee(String currency) {
        return targetCurrencyFee.reduce(bank, currency);
    }

    public Money getTargetServiceFee(String currency) {
        return targetServiceFee.reduce(bank, currency);
    }

    public Money getTargetAmountAfterRateFee(String currency) {
        return targetAmountAfterRateFee.reduce(bank, currency);
    }

    public Money getTotalTargetFees(String currency) {
        return totalTargetFees.reduce(bank, currency);
    }

    public Money getTotalTransactionFees(String currency) {
        return totalTransactionFees.reduce(bank, currency);
    }

    public Money getTotalTransactionAmount(String currency) {
        return totalTransactionAmount.reduce(bank, currency);
    }

    public BigDecimal getTargetCurrencyRateFeePerc() {
        return targetCurrencyRateFeePerc;
    }


}

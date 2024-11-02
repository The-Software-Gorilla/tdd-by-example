package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class CurrencyTransaction {
    private final Bank bank;
    private final Money sourceAmount;
    private final String targetCurrency;

    private Money sourceFee;
    private BigDecimal targetCurrencyRateFeePercentage;
    private BigDecimal targetConversionRate;
    private Money targetAmountAfterRateFee;
    private Money targetCurrencyFee;


    private Money targetServiceFee;
    private Money totalTargetFees;
    private Money totalTransactionFees;
    private Money totalTransactionAmount;
    private Money settlementAmount;
    private boolean isSettled;

    public CurrencyTransaction(Bank bank, Money sourceAmount, String targetCurrency) {
        this.bank = bank;
        this.sourceAmount = sourceAmount;
        this.targetCurrency = targetCurrency;
        isSettled = false;
        sourceFee = new Money(BigDecimal.ZERO, sourceAmount.getCurrency());
        targetCurrencyRateFeePercentage = BigDecimal.ZERO;
        targetConversionRate = BigDecimal.ZERO;
        targetAmountAfterRateFee = new Money(BigDecimal.ZERO, targetCurrency);
        targetCurrencyFee = new Money(BigDecimal.ZERO, targetCurrency);
        targetServiceFee = new Money(BigDecimal.ZERO, targetCurrency);
        totalTargetFees = new Money(BigDecimal.ZERO, targetCurrency);
        totalTransactionFees = new Money(BigDecimal.ZERO, targetCurrency);
        totalTransactionAmount = new Money(BigDecimal.ZERO, sourceAmount.getCurrency());
        settlementAmount = new Money(BigDecimal.ZERO, targetCurrency);
    }

    public void setSourceFee(Money fee) {
        this.sourceFee = fee;
    }

    public Money getSourceFee() {
        return sourceFee;
    }

    public void setTargetCurrencyRateFeePercentage(BigDecimal rate) {
        this.targetCurrencyRateFeePercentage = rate;
    }

    public void setTargetServiceFee(Money fee) {
        this.targetServiceFee = fee;
    }

    public boolean isSettled() {
        return isSettled;
    }

    public void settle() {
        if (isSettled) {
            return;
        }
        BigDecimal bankRate = bank.rate(sourceAmount.getCurrency(),targetCurrency);
        BigDecimal rateFee = bankRate.multiply(targetCurrencyRateFeePercentage).setScale(8, RoundingMode.HALF_UP);
        this.targetConversionRate = bankRate.subtract(rateFee).setScale(8, RoundingMode.HALF_UP);
        this.targetAmountAfterRateFee = new Money(sourceAmount.getAmount().multiply(targetConversionRate), targetCurrency);
        Money srcInDest = sourceAmount.reduce(bank, targetCurrency);
        this.targetCurrencyFee = new Money(srcInDest.getAmount().subtract(targetAmountAfterRateFee.getAmount()), targetCurrency);
        this.totalTargetFees = targetCurrencyFee.plus(targetServiceFee).reduce(bank, targetCurrency);
        this.totalTransactionFees = sourceFee.plus(totalTargetFees).reduce(bank, targetCurrency);
        this.totalTransactionAmount = sourceAmount.plus(this.sourceFee).reduce(bank, sourceAmount.getCurrency());
        this.settlementAmount = new Money(this.targetAmountAfterRateFee.getAmount().subtract(targetServiceFee.getAmount()), targetCurrency);
        isSettled = true;
    }

    public BigDecimal getTargetConversionRate() {
        return targetConversionRate;
    }

    public Money getSettlementAmount() {
        return settlementAmount;
    }

    public Money getTargetCurrencyFee() {
        return targetCurrencyFee;
    }

    public Money getTargetServiceFee() {
        return targetServiceFee;
    }

    public Money getTargetAmountAfterRateFee() {
        return targetAmountAfterRateFee;
    }

    public Money getTotalTargetFees() {
        return totalTargetFees;
    }

    public Money getTotalTransactionFees() {
        return totalTransactionFees;
    }

    public Money getTotalTransactionAmount() {
        return totalTransactionAmount;
    }

    public BigDecimal getTargetCurrencyRateFeePercentage() {
        return targetCurrencyRateFeePercentage;
    }


}

package com.thesoftwaregorilla.tdd.money;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class CurrencyTransaction {
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

    public CurrencyTransaction(Money sourceAmount, String targetCurrency) {
        this.sourceAmount = sourceAmount;
        this.targetCurrency = targetCurrency;
        isSettled = false;
        sourceFee = Money.from(BigDecimal.ZERO, sourceAmount.getCurrency(), sourceAmount.getBank());
        targetCurrencyRateFeePercentage = BigDecimal.ZERO;
        targetConversionRate = BigDecimal.ZERO;
        targetAmountAfterRateFee = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
        targetCurrencyFee = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
        targetServiceFee = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
        totalTargetFees = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
        totalTransactionFees = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
        totalTransactionAmount = Money.from(BigDecimal.ZERO, sourceAmount.getCurrency(), sourceAmount.getBank());
        settlementAmount = Money.from(BigDecimal.ZERO, targetCurrency, sourceAmount.getBank());
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
        BigDecimal bankRate = this.sourceAmount.getBank().rate(sourceAmount.getCurrency(),targetCurrency);
        BigDecimal rateFee = bankRate.multiply(targetCurrencyRateFeePercentage).setScale(8, RoundingMode.HALF_UP);
        this.targetConversionRate = bankRate.subtract(rateFee).setScale(8, RoundingMode.HALF_UP);
        this.targetAmountAfterRateFee = Money.from(sourceAmount.getAmount().multiply(targetConversionRate), targetCurrency, sourceAmount.getBank());
        Money srcInDest = sourceAmount.convert(targetCurrency);
        this.targetCurrencyFee = Money.from(srcInDest.subtract(targetAmountAfterRateFee).getAmount(), targetCurrency, sourceAmount.getBank());
        this.totalTargetFees = targetCurrencyFee.add(targetServiceFee);
        this.totalTransactionFees = sourceFee.convert(targetCurrency).add(totalTargetFees);
        this.totalTransactionAmount = sourceAmount.add(sourceFee);
        this.settlementAmount = Money.from(targetAmountAfterRateFee.subtract(targetServiceFee).getAmount(), targetCurrency, sourceAmount.getBank());
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

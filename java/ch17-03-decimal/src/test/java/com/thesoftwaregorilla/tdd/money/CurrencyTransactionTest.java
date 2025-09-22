package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;

import java.math.BigDecimal;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//</editor-fold>

public class CurrencyTransactionTest {

    private static Bank bank;
    @BeforeAll
    public static void beforeAllSetUp() {
        bank = new Bank();
        bank.addRate("USD", "ZAR",new BigDecimal("17.64"));
        bank.addRate("ZAR", "USD",new BigDecimal("0.056689"));
    }

    @Test
    @DisplayName("currency transaction")
    public void testCurrencyTransaction() {
        CurrencyTransaction transaction = new CurrencyTransaction(bank, Money.dollar(new BigDecimal("1000.00")), "ZAR");
        transaction.setSourceFee(Money.dollar(new BigDecimal("35.00")));
        transaction.setTargetCurrencyRateFeePercentage(new BigDecimal("0.015"));
        transaction.setTargetServiceFee(Money.rand(new BigDecimal("150.00")));
        transaction.settle();
        assertEquals(new BigDecimal("17.37540000"), transaction.getTargetConversionRate());
        assertEquals(new BigDecimal("15.00"), transaction.getTargetCurrencyFee().getValueIn("USD", bank));
        assertEquals(new BigDecimal("264.60"), transaction.getTargetCurrencyFee().getValueIn("ZAR", bank));
        assertEquals(new BigDecimal("8.50"), transaction.getTargetServiceFee().getValueIn("USD", bank));
        assertEquals(new BigDecimal("150.00"), transaction.getTargetServiceFee().getValueIn("ZAR", bank));
        assertEquals(new BigDecimal("23.50"), transaction.getTotalTargetFees().getValueIn("USD", bank));
        assertEquals(new BigDecimal("414.60"), transaction.getTotalTargetFees().getValueIn("ZAR", bank));
        assertEquals(new BigDecimal("976.49"), transaction.getSettlementAmount().getValueIn("USD", bank));
        assertEquals(new BigDecimal("17225.40"), transaction.getSettlementAmount().getValueIn("ZAR", bank));
        assertEquals(new BigDecimal("58.50"), transaction.getTotalTransactionFees().getValueIn("USD", bank));
        assertEquals(new BigDecimal("1032.00"), transaction.getTotalTransactionFees().getValueIn("ZAR", bank));
        assertEquals(new BigDecimal("1035.00"), transaction.getTotalTransactionAmount().getValueIn("USD", bank));
        assertEquals(new BigDecimal("18257.40"), transaction.getTotalTransactionAmount().getValueIn("ZAR", bank));
    }

    @Test
    @DisplayName("unsettled transaction")
    public void testUnsettledTransaction() {
        CurrencyTransaction transaction = new CurrencyTransaction(bank, Money.dollar(new BigDecimal("1000.00")), "ZAR");
        assertFalse(transaction.isSettled());
        assertEquals(Money.dollar(BigDecimal.ZERO), transaction.getSourceFee());
        assertEquals(BigDecimal.ZERO, transaction.getTargetCurrencyRateFeePercentage());
        assertEquals(Money.rand(BigDecimal.ZERO), transaction.getTargetServiceFee());
        assertEquals(BigDecimal.ZERO, transaction.getTargetConversionRate());
        assertEquals(Money.rand(BigDecimal.ZERO), transaction.getTargetCurrencyFee());
        assertEquals(Money.rand(BigDecimal.ZERO), transaction.getTotalTargetFees());
        assertEquals(Money.rand(BigDecimal.ZERO), transaction.getSettlementAmount());
        assertEquals(Money.rand(BigDecimal.ZERO), transaction.getTotalTransactionFees());
        assertEquals(Money.dollar(BigDecimal.ZERO), transaction.getTotalTransactionAmount());
    }

}

package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;

import java.math.BigDecimal;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//</editor-fold>

public class CurrencyTransactionTest {

    private static Bank<Money> bank;
    @BeforeAll
    public static void beforeAllSetUp() {
        bank = new Bank<>();
        bank.addRate("USD", "ZAR",new BigDecimal("17.64"));
    }

    @Test
    @DisplayName("currency transaction")
    public void testCurrencyTransaction() {
        CurrencyTransaction transaction = new CurrencyTransaction(Money.from(new BigDecimal("1000.00"), "USD", bank), "ZAR");
        transaction.setSourceFee(Money.from(new BigDecimal("35.00"), "USD", bank));
        transaction.setTargetCurrencyRateFeePercentage(new BigDecimal("0.015"));
        transaction.setTargetServiceFee(Money.from(new BigDecimal("150.00"), "ZAR", bank));
        transaction.settle();
        assertEquals(new BigDecimal("17.37540000"), transaction.getTargetConversionRate());
        assertEquals(new BigDecimal("15.00"), transaction.getTargetCurrencyFee().getAmountIn("USD"));
        assertEquals(new BigDecimal("264.60"), transaction.getTargetCurrencyFee().getAmountIn("ZAR"));
        assertEquals(new BigDecimal("8.50"), transaction.getTargetServiceFee().getAmountIn("USD"));
        assertEquals(new BigDecimal("150.00"), transaction.getTargetServiceFee().getAmountIn("ZAR"));
        assertEquals(new BigDecimal("23.50"), transaction.getTotalTargetFees().getAmountIn("USD"));
        assertEquals(new BigDecimal("414.60"), transaction.getTotalTargetFees().getAmountIn("ZAR"));
        assertEquals(new BigDecimal("976.50"), transaction.getSettlementAmount().getAmountIn("USD"));
        assertEquals(new BigDecimal("17225.40"), transaction.getSettlementAmount().getAmountIn("ZAR"));
        assertEquals(new BigDecimal("58.50"), transaction.getTotalTransactionFees().getAmountIn("USD"));
        assertEquals(new BigDecimal("1032.00"), transaction.getTotalTransactionFees().getAmountIn("ZAR"));
        assertEquals(new BigDecimal("1035.00"), transaction.getTotalTransactionAmount().getAmountIn("USD"));
        assertEquals(new BigDecimal("18257.40"), transaction.getTotalTransactionAmount().getAmountIn("ZAR"));
    }

    @Test
    @DisplayName("unsettled transaction")
    public void testUnsettledTransaction() {
        CurrencyTransaction transaction = new CurrencyTransaction(Money.from(new BigDecimal("1000.00"), "USD", bank), "ZAR");
        assertFalse(transaction.isSettled());
        assertEquals(Money.from(BigDecimal.ZERO, "USD", bank), transaction.getSourceFee());
        assertEquals(BigDecimal.ZERO, transaction.getTargetCurrencyRateFeePercentage());
        assertEquals(Money.from(BigDecimal.ZERO, "ZAR", bank), transaction.getTargetServiceFee());
        assertEquals(BigDecimal.ZERO, transaction.getTargetConversionRate());
        assertEquals(Money.from(BigDecimal.ZERO, "ZAR", bank), transaction.getTargetCurrencyFee());
        assertEquals(Money.from(BigDecimal.ZERO, "ZAR", bank), transaction.getTotalTargetFees());
        assertEquals(Money.from(BigDecimal.ZERO, "ZAR", bank), transaction.getSettlementAmount());
        assertEquals(Money.from(BigDecimal.ZERO, "ZAR", bank), transaction.getTotalTransactionFees());
        assertEquals(Money.from(BigDecimal.ZERO, "USD", bank), transaction.getTotalTransactionAmount());
    }

}

package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;

import java.math.BigDecimal;

import static org.junit.jupiter.api.Assertions.assertEquals;

//<editor-fold desc="TO DO List">
//TODO: Add support for subtracting two Money objects
//TODO: Clean up the settle() method in CurrencyTransaction
//TODO: Change the tests to do more assert expected value calculations
//TODO: Add 5 test cases
//TODO: Refactor the tests for separating the asserts
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
        transaction.setTargetCurrencyRateFeePerc(new BigDecimal("0.015"));
        transaction.setTargetServiceFee(Money.rand(new BigDecimal("150.00")));
        transaction.settle();
        assertEquals(new BigDecimal("17.37540000"), transaction.getTargetConversionRate());
        assertEquals(new BigDecimal("15.00"), transaction.getTargetCurrencyFee("USD").getAmount());
        assertEquals(new BigDecimal("264.60"), transaction.getTargetCurrencyFee("ZAR").getAmount());
        assertEquals(new BigDecimal("8.50"), transaction.getTargetServiceFee("USD").getAmount());
        assertEquals(new BigDecimal("150.00"), transaction.getTargetServiceFee("ZAR").getAmount());
        assertEquals(new BigDecimal("23.50"), transaction.getTotalTargetFees("USD").getAmount());
        assertEquals(new BigDecimal("414.60"), transaction.getTotalTargetFees("ZAR").getAmount());
        assertEquals(new BigDecimal("976.49"), transaction.getSettlementAmount("USD").getAmount());
        assertEquals(new BigDecimal("17225.40"), transaction.getSettlementAmount("ZAR").getAmount());
        assertEquals(new BigDecimal("58.50"), transaction.getTotalTransactionFees("USD").getAmount());
        assertEquals(new BigDecimal("1032.00"), transaction.getTotalTransactionFees("ZAR").getAmount());
        assertEquals(new BigDecimal("1035.00"), transaction.getTotalTransactionAmount("USD").getAmount());
        assertEquals(new BigDecimal("18257.40"), transaction.getTotalTransactionAmount("ZAR").getAmount());
    }
}

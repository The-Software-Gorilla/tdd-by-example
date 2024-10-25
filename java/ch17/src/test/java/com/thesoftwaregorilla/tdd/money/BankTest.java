package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

//<editor-fold desc="TO DO List">
//TODO: Add test for reducing money to different currencies.
//</editor-fold>


@DisplayName("Bank Tests")
public class BankTest {

    private static Bank bank;

    @BeforeAll
    public static void beforeAllSetUp() {
        bank = getBankWithRates();
    }

    public static Bank getBankWithRates() {
        if (bank == null) {
            bank = new Bank();
            bank.addRate("CHF", "USD", 2); // 1 CHF = 0.5 USD
            bank.addRate("ZAR", "USD", 17); // 1 ZAR = 0.0588 USD
            bank.addRate("ZAR", "CHF", 20); // 1 ZAR = 0.05 CHF
        }
        return bank;
    }

    @DisplayName("reduce")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\", expected = {3}")
    @CsvSource({
            "CHF, 2, USD, 1",
            "ZAR, 20, CHF, 1",
            "ZAR, 17, USD, 1",
            "USD, 5, USD, 5"
            })
    public void testReduce(String from, int fromAmt, String to, int expected) {
        Money result = bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        assertEquals(MoneyTest.getCurrencyFactory(to).apply(expected), result);
    }

    @DisplayName("reduce with division by zero exception")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\"")
    @CsvSource({
            "USD, 1, ZAR, 20",
            "CHF, 1, ZAR, 20",
            "USD, 1, CHF, 0"
    })
    public void testReduceWithDbzException(String from, int fromAmt, String to, int expected) {
        ArithmeticException exception = assertThrows(ArithmeticException.class, () -> {
            bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        });
        assertEquals("/ by zero", exception.getMessage());
    }

    // This error occurs when the currency does not have a factory method.
    @DisplayName("reduce with null pointer exception")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\"")
    @CsvSource({
            "GBP, 1, ZAR, 20",
            "INR, 1, ZAR, 20",
            "FRF, 1, CHF, 0"
    })
    public void testReduceWithNpException(String from, int fromAmt, String to, int expected) {
        NullPointerException exception = assertThrows(NullPointerException.class, () -> {
            bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        });
    }

    @DisplayName("rate table")
    @ParameterizedTest(name = "rate from = \"{0}\" to = \"{1}\", expected = {2}")
    @CsvSource({
            "USD, USD, 1",
            "CHF, CHF, 1",
            "ZAR, ZAR, 1",
            "USD, CHF, 0",
            "CHF, ZAR, 0",
            "GBP, ZAR, 0",
            "CHF, USD, 2",
            "ZAR, USD, 17",
            "ZAR, CHF, 20"
    })
    public void testIdentityRate(String from, String to, int expected) {
        assertEquals(expected, bank.rate(from, to));
    }

}

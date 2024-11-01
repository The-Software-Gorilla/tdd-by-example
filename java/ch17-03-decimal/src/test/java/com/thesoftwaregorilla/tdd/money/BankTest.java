package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.math.BigDecimal;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

//<editor-fold desc="TO DO List">
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
            bank.addRate("CHF", "USD", BigDecimal.valueOf(2)); // 1 CHF = 0.5 USD
            bank.addRate("ZAR", "USD", BigDecimal.valueOf(17)); // 1 ZAR = 0.0588 USD
            bank.addRate("ZAR", "CHF", BigDecimal.valueOf(20)); // 1 ZAR = 0.05 CHF
            bank.addRate("USD", "CHF", BigDecimal.valueOf(0.5)); // 1 CHF = 0.5 USD
            bank.addRate("USD", "ZAR", BigDecimal.valueOf(0.0588235)); // 1 ZAR = 0.0588 USD
        }
        return bank;
    }

    @DisplayName("rate table")
    @ParameterizedTest(name = "rate from = \"{0}\" to = \"{1}\", expected = {2}")
    @CsvSource({
            "USD, USD, 1.00000000",
            "CHF, CHF, 1.00000000",
            "ZAR, ZAR, 1.00000000",
            "USD, CHF, 0.50000000",
            "CHF, ZAR, 0E-8",
            "GBP, ZAR, 0E-8",
            "CHF, USD, 2.00000000",
            "ZAR, USD, 17.00000000",
            "USD, ZAR, 0.05882350",
            "ZAR, CHF, 20.00000000"
    })
    public void testRate(String from, String to, BigDecimal expected) {
        assertEquals(expected, bank.rate(from, to));
    }


    @DisplayName("reduce")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\", expected = {3}")
    @CsvSource({
            "CHF, 2, USD, 1",
            "ZAR, 20, CHF, 1",
            "ZAR, 17, USD, 1",
            "USD, 5, USD, 5"
            })
    public void testReduce(String from, BigDecimal fromAmt, String to, BigDecimal expected) {
        Money result = bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        assertEquals(MoneyTest.getCurrencyFactory(to).apply(expected), result);
    }

    @DisplayName("reduce with division by zero exception")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\"")
    @CsvSource({
            "CHF, 1, GBP, 17",
            "ZAR, 1, GBP, 20",
            "USD, 1, INR, 0"
    })
    public void testReduceWithDbzException(String from, BigDecimal fromAmt, String to, int expected) {
        ArithmeticException exception = assertThrows(ArithmeticException.class, () -> {
            bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        });
        assertEquals("Exchange rate not available", exception.getMessage());
    }

    // This error occurs when the currency does not have a factory method.
    @DisplayName("reduce with null pointer exception")
    @ParameterizedTest(name = "reduce from = \"{0}\", amount = {1}, to = \"{2}\"")
    @CsvSource({
            "GBP, 1, ZAR, 20",
            "INR, 1, ZAR, 20",
            "FRF, 1, CHF, 0"
    })
    public void testReduceNotInRateTable(String from, BigDecimal fromAmt, String to, int expected) {
        NullPointerException exception = assertThrows(NullPointerException.class, () -> {
            bank.reduce(MoneyTest.getCurrencyFactory(from).apply(fromAmt), to);
        });
    }


}

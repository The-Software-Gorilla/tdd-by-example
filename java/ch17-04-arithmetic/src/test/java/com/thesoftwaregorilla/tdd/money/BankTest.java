package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
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

    private Bank<Money> bank;
    @BeforeEach
    public void beforeEach() {
        bank = getBankWithRates();
    }

    @AfterEach
    public void afterEach() {
        bank = null;
    }

    public static Bank<Money> getBankWithRates() {
        Bank<Money> bank = new Bank<Money>();
        bank.addRate("USD", "CHF", BigDecimal.valueOf(2));
        bank.addRate("USD", "ZAR", BigDecimal.valueOf(17));
        bank.addRate("CHF", "ZAR", BigDecimal.valueOf(20));
        return bank;
    }

    @DisplayName("rate table")
    @ParameterizedTest(name = "rate from = \"{0}\" to = \"{1}\", expected = {2}")
    @CsvSource({
            "USD, USD, 1.00000000",
            "CHF, CHF, 1.00000000",
            "ZAR, ZAR, 1.00000000",
            "USD, CHF, 2.00000000",
            "CHF, ZAR, 20.00000000",
            "GBP, ZAR, 0E-8",
            "CHF, USD, 0.50000000",
            "ZAR, USD, 0.05882353",
            "USD, ZAR, 17.00000000",
            "ZAR, CHF, 0.05000000"
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
        Money result = bank.convert(Money.from(fromAmt, from, bank), to);
        assertEquals(Money.from(expected, to, bank), result);
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
            bank.convert(Money.from(fromAmt, from, bank), to);
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
        ArithmeticException exception = assertThrows(ArithmeticException.class, () -> {
            bank.convert(Money.from(fromAmt, from, bank), to);
        });
    }


}

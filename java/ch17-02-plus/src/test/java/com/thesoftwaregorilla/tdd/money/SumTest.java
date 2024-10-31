package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: Look into refactoring the code in Sum to use BinaryOperator and/or UnaryOperator instead of Expression.
// (see issue #1)
//TODO: Refactor the testPlus and testTimes methods to use a single method with parameters for the operation type.
// (needs to wait for plus cleanup)
//</editor-fold>

@DisplayName("Sum Class Tests")
class SumTest {

    @DisplayName("reduce same currency")
    @ParameterizedTest(name = "{0} : {1}, {2} : {3}, Expected: {4}")
    @CsvSource({
            "USD, 5, USD, 5, 10",
            "CHF, 5, CHF, 5, 10",
            "ZAR, 5, ZAR, 5, 10"
    })
    void testReduceSameCurrency(String fromCurrency, int fromAmount, String toCurrency, int toAmount, int expectedAmount) {
        testReduce(fromCurrency, fromAmount, toCurrency, toAmount, expectedAmount);
    }

    @DisplayName("reduce different currency")
    @ParameterizedTest(name = "{0} : {1}, {2} : {3}, Expected: {4}")
    @CsvSource({
            "CHF, 10, USD, 5, 10",
            "ZAR, 20, CHF, 5, 6",
            "ZAR, 34, USD, 5, 7"
    })
    void testReduceDiffCurrency(String fromCurrency, int fromAmount, String toCurrency, int toAmount, int expectedAmount) {
        testReduce(fromCurrency, fromAmount, toCurrency, toAmount, expectedAmount);
    }

    private void testReduce(String fromCurrency, int fromAmount, String toCurrency, int toAmount, int expectedAmount) {
        Sum sum = new Sum(MoneyTest.getCurrencyFactory(fromCurrency).apply(fromAmount),
                MoneyTest.getCurrencyFactory(toCurrency).apply(toAmount));
        Money reduced = sum.reduce(BankTest.getBankWithRates(), toCurrency);
        assertEquals(MoneyTest.getCurrencyFactory(toCurrency).apply(expectedAmount), reduced);
    }

    @DisplayName("plus returning a sum (testPlusReturnsSum method in TDD book was part of MoneyTest)")
    @ParameterizedTest(name = "{0} : {1}")
    @CsvSource({
            "USD, 5",
            "CHF, 5",
            "ZAR, 5"
    })
    public void testPlusReturnsSum(String currency, int amount) {
        Money money = MoneyTest.getCurrencyFactory(currency).apply(amount);
        Expression result = money.plus(money);
        assertInstanceOf(Sum.class, result);
        Sum sum = (Sum) result;
        assertEquals(money, sum.augend);
        assertEquals(money, sum.addend);
    }


    @DisplayName("plus")
    @ParameterizedTest(name = "{0} : {1}")
    @CsvSource({
            "USD, 5",
            "CHF, 7",
            "ZAR, 10"
    })
    void testPlus(String currency, int amount) {
        final int expectedAmount = amount * 3;
        Expression money = MoneyTest.getCurrencyFactory(currency).apply(amount);
        Sum sum = new Sum(money, money);
        Expression result = sum.plus(money);
        assertInstanceOf(Sum.class, result);
        assertEquals(MoneyTest.getCurrencyFactory(currency).apply(expectedAmount), result.reduce(BankTest.getBankWithRates(), currency));
    }

    @DisplayName("times")
    @ParameterizedTest(name = "{0} : {1}, Multiplier: {2}")
    @CsvSource({
            "USD, 5, 3",
            "CHF, 7, 5",
            "ZAR, 10, 30"
    })
    void testTimes(String currency, int amount, int multiplier) {
        final int expectedAmount = (amount + amount) * multiplier;
        Expression money = MoneyTest.getCurrencyFactory(currency).apply(amount);
        Sum sum = new Sum(money, money);
        Expression result = sum.times(multiplier);
        assertInstanceOf(Sum.class, result);
        assertEquals(MoneyTest.getCurrencyFactory(currency).apply(expectedAmount), result.reduce(BankTest.getBankWithRates(), currency));
    }

}
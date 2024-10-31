package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.util.function.BiFunction;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
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
            "USD, CHF, 5",
            "CHF, CHF, 5",
            "ZAR, USD, 5"
    })
    public void testPlusReturnsSum(String baseCurrency, String targetCurrency, int amount) {
        Money baseMoney = MoneyTest.getCurrencyFactory(baseCurrency).apply(amount);
        Money targetMoney = MoneyTest.getCurrencyFactory(targetCurrency).apply(amount);
        Expression result = baseMoney.plus(targetMoney);
        if(baseMoney.getCurrency().equals(targetMoney.getCurrency())) {
            assertInstanceOf(Money.class, result);
            Money money = (Money) result;
            assertEquals(baseMoney.getAmount() + targetMoney.getAmount(), money.getAmount());
            assertEquals(baseMoney.getCurrency(), money.getCurrency());
        } else {
            assertInstanceOf(Sum.class, result);
            Sum sum = (Sum) result;
            assertEquals(baseMoney, sum.augend);
            assertEquals(targetMoney, sum.addend);
        }
    }


    @DisplayName("plus")
    @ParameterizedTest(name = "{0} : {1} + {2}")
    @CsvSource({
            "USD, 5, 4",
            "CHF, 7, 9",
            "ZAR, 10, 23"
    })
    void testPlus(String currency, int amount, int extraAmount) {
        final int expectedAmount = (amount * 2) + extraAmount;
        testArithmetic(currency, amount, extraAmount, expectedAmount, (money, amt) -> money.plus(MoneyTest.getCurrencyFactory(currency).apply(amt)) );
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
        testArithmetic(currency, amount, multiplier, expectedAmount, Expression::times);
    }

    private void testArithmetic(String currency, int amount, int extraAmount, int expectedAmount, BiFunction<Expression, Integer, Expression> operation) {
        Expression money = MoneyTest.getCurrencyFactory(currency).apply(amount);
        Sum sum = new Sum(money, money);
        Expression result = operation.apply(sum, extraAmount);
        assertEquals(MoneyTest.getCurrencyFactory(currency).apply(expectedAmount), result.reduce(BankTest.getBankWithRates(), currency));
    }
}
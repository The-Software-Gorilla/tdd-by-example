package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.util.HashMap;
import java.util.function.Function;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: Add tests to deal with division by zero exceptions in the Money class.
//TODO: Clean up the arithmetic tests align augend and addend with from and to currency pairs.
//TODO: Refactor these tests into a nested test structure for the Money class. Basic tests first, then currency arithmetic and conversions.
//</editor-fold>


@DisplayName("Money Class Tests")
public class MoneyTest {

    //<editor-fold desc="Test Setup">
    private static HashMap<String, Function<Integer, Money>> currencyFactories;
    private static final int STD_AMOUNT = 5;
    private static Bank bank;

    @BeforeAll
    public static void beforeAllSetUp() {
        getCurrencyFactories();
        bank = BankTest.getBankWithRates();
    }

    private static HashMap<String, Function<Integer, Money>> getCurrencyFactories() {
        if(currencyFactories == null) {
            currencyFactories = new HashMap<>();
            currencyFactories.put("USD", Money::dollar);
            currencyFactories.put("CHF", Money::franc);
            currencyFactories.put("ZAR", Money::rand);
        }
        return currencyFactories;
    }

    public static Function<Integer, Money> getCurrencyFactory(String currency) {
        return getCurrencyFactories().get(currency);
    }
    //</editor-fold>


    @DisplayName("Money Construction")
    @ParameterizedTest(name = "Money Construction with currency = \"{0}\"")
    @CsvSource({
            "USD",
            "CHF",
            "ZAR"
    })
    public void testConstruction(String currency) {
        Money money = currencyFactories.get(currency).apply(STD_AMOUNT);
        assertNotNull(money);
        assertEquals(STD_AMOUNT, money.getAmount());
        assertEquals(currency, money.getCurrency());
    }

    @DisplayName("Equality")
    @ParameterizedTest(name = "Equality with currency = \"{0}\", unequal currency = \"{1}\", unequal amount = {2}")
    @CsvSource({
            "USD, CHF, 6",
            "CHF, ZAR, 7",
            "ZAR, USD, 8"
    })
    public void testEquality(String currency, String unequalCurrency, int unequalAmount) {
        Money money = currencyFactories.get(currency).apply(STD_AMOUNT);
        assertEquals(money, currencyFactories.get(currency).apply(STD_AMOUNT));
        assertNotEquals(money, currencyFactories.get(currency).apply(unequalAmount));
        assertNotEquals(money, currencyFactories.get(unequalCurrency).apply(STD_AMOUNT));
    }


    @DisplayName("Currency")
    @ParameterizedTest(name = "Currency with currency = \"{0}\"")
    @CsvSource ({
            "USD",
            "CHF",
            "ZAR"
    })
    public void testCurrency(String currency) {
        assertEquals(currency, currencyFactories.get(currency).apply(STD_AMOUNT).getCurrency());
    }

    @DisplayName("Multiplication for aliasing")
    @ParameterizedTest(name = "Multiplication with currency = \"{0}\", multiplier = {1},{2}")
    @CsvSource({
            "USD, 2, 4",
            "CHF, 3, 2",
            "ZAR, 4, 3"
    })
   public void testMultiplication(String currency, int multiplier1, int multiplier2) {
        Money money = currencyFactories.get(currency).apply(STD_AMOUNT);
        assertEquals(new Money(STD_AMOUNT * multiplier1, currency), money.times(multiplier1));
        assertEquals(new Money(STD_AMOUNT * multiplier2, currency), money.times(multiplier2));
    }

    @DisplayName("Simple Addition")
    @ParameterizedTest(name = "Simple Addition with currency = \"{0}\"")
    @CsvSource({
            "USD",
            "CHF",
            "ZAR"
    })
    public void testSimpleAddition(String currency) {
        Money money = currencyFactories.get(currency).apply(STD_AMOUNT);
        Expression sum = money.plus(money);
        Bank bank = new Bank();
        Money reduced = bank.reduce(sum, currency);
        assertEquals(new Money(STD_AMOUNT + STD_AMOUNT, currency), reduced);
    }


    @Test
    @DisplayName("Reduce Money")
    public void testReduceMoney() {
        Bank bank = new Bank();
        Money result = bank.reduce(Money.dollar(1), "USD");
        assertEquals(Money.dollar(1), result);
    }

    @Test
    @DisplayName("Reduce Sum")
    public void testReduceSum() {
        Expression sum = new Sum(Money.dollar(3), Money.dollar(4));
        Bank bank = new Bank();
        Money result = bank.reduce(sum, "USD");
        assertEquals(Money.dollar(7), result);
    }

    @DisplayName("Reduce Money with Different Currencies")
    @ParameterizedTest(name = "Reduce Money with Different Currencies from \"{0}\" to \"{1}\". Rate = {2}, Amount = {3}")
    @CsvSource({
            "CHF, USD, 2, 2",
            "ZAR, USD, 17, 85",
            "ZAR, CHF, 20, 60"
    })
    public void testReduceMoneyDifferentCurrency(String fromCurrency, String toCurrency, int rate, int amount) {
        Bank bank = new Bank();
        bank.addRate(fromCurrency, toCurrency, rate);
        Money result = bank.reduce(currencyFactories.get(fromCurrency).apply(amount), toCurrency);
        assertEquals(currencyFactories.get(toCurrency).apply(amount / rate), result);
    }


    @Test
    public void testMixedAddition() {
        Expression fiveBucks = Money.dollar(STD_AMOUNT);
        Expression tenFrancs = Money.franc(10);
        Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
        assertEquals(Money.dollar(10), result);
    }

    @DisplayName("Sum Plus")
    @ParameterizedTest(name = "Sum Plus with augend = \"{0}\", augendAmt = {1}, addend = \"{2}\", addendAmt = {3}, expected = {4}")
    @CsvSource({
            "USD, 5, CHF, 10, 15",
            "CHF, 10, ZAR, 20, 21",
            "USD, 5, ZAR, 34, 12"
    })
    public void testSumPlusMoney(String augend, int augendAmt, String addend, int addendAmt, int expected) {
        testReduceHarness(augend, augendAmt, addend, addendAmt, expected,
                (aug, add) -> new Sum(aug, add).plus(aug));
    }


    @DisplayName("Sum Times")
    @ParameterizedTest(name = "Sum Times with augend = \"{0}\", augendAmt = {1}, addend = \"{2}\", addendAmt = {3}, expected = {4}")
    @CsvSource({
            "USD, 5, CHF, 10, 20",
            "CHF, 10, ZAR, 20, 22",
            "USD, 5, ZAR, 85, 20"
    })
    public void testSumTimes(String augend, int augendAmt, String addend, int addendAmt, int expected) {
        testReduceHarness(augend, augendAmt, addend, addendAmt, expected,
                (aug, add) -> new Sum(aug, add).times(2));
    }

    private void testReduceHarness(String augend, int augendAmt, String addend, int addendAmt, int expected, ITestOperation operation) {
        Expression money1 = currencyFactories.get(augend).apply(augendAmt);
        Expression money2 = currencyFactories.get(addend).apply(addendAmt);
        Expression sum = operation.apply(money1, money2);
        Money result = bank.reduce(sum, augend);
        assertEquals(currencyFactories.get(augend).apply(expected), result);
    }


    // This test is discussed at the end of Chapter 16, but there is no clean implementation for how to fix make it pass in the book.
    // The test is commented out because it will fail. We'll come back to it later.
    @Test
    @DisplayName("Plus Same Currency Returns Money")
    @Disabled("Disabled until we implement the fix for duplicate plus implementation")
    public void testPlusSameCurrencyReturnsMoney() {
        Expression sum = Money.dollar(1).plus(Money.dollar(1));
        assertInstanceOf(Money.class, sum);
    }

}

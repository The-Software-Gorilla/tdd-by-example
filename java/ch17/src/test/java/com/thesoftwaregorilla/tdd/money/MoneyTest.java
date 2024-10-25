package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.util.HashMap;
import java.util.function.Function;

import static org.junit.jupiter.api.Assertions.*;

public class MoneyTest {


    private static HashMap<String, Function<Integer, Money>> currencyFactories;
    private static final int STD_AMOUNT = 5;
    private static Bank bank;

    @BeforeAll
    public static void beforeAllSetUp() {
        currencyFactories = new HashMap<>();
        currencyFactories.put("USD", Money::dollar);
        currencyFactories.put("CHF", Money::franc);
        currencyFactories.put("ZAR", Money::rand);
        bank = getBankWithRates();
    }

    private static Bank getBankWithRates() {
        Bank bank = new Bank();
        bank.addRate("CHF", "USD", 2); // 1 CHF = 0.5 USD
        bank.addRate("ZAR", "USD", 17); // 1 ZAR = 0.0588 USD
        bank.addRate("ZAR", "CHF", 20); // 1 ZAR = 0.05 CHF
        return bank;
    }


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
    @DisplayName("Addition Returning a Sum")
    public void testPlusReturnsSum() {
        Money fiveDollar = Money.dollar(STD_AMOUNT);
        Expression result = fiveDollar.plus(fiveDollar);
        Sum sum = (Sum) result;
        assertEquals(fiveDollar, sum.augend);
        assertEquals(fiveDollar, sum.addend);
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

    // On page 69, Kent suggests trying this to see if the test will pass. In 2002, Java did not have
    // a built-in array equality method test, so the code in the book would not compile, and still fails as a test.
    // The Java code he suggested then is:
    // assertEquals(new Object[] {"abc"}, new Object[] {"abc"});
    // JUnit 5 supports array comparison in 2024, so this test will pass and I am leaving it here for reference.
    @Test
    @DisplayName("Array Equals test for reference")
    @Disabled("Test exists to illustrate a change to JUnit since the TDD By Example book was published.")
    public void testArrayEquals() {
//        assertEquals(new Object[] {"abc"}, new Object[] {"abc"}); // This test will fail.
        var expected = new Object[] { "abc" };
        var actual = new Object[] { "abc" };
        assertArrayEquals(actual, expected);
    }

    // This code is not in the book, but when I started adding the CurrencyPair class, I realized we should have
    // started with a test.
    @Test
    @DisplayName("CurrencyPair Equals")
    public void testCurrencyPairEquals() {
        var francToDollar = new CurrencyPair("CHF", "USD");
        assertEquals(francToDollar, new CurrencyPair("CHF", "USD"));
        assertEquals(francToDollar.hashCode(), new CurrencyPair("CHF", "USD").hashCode());
    }

    @DisplayName("Unmatched CurrencyPair Equals")
    @ParameterizedTest(name = "Unmatched CurrencyPair Equals with from = \"{0}\", to = \"{1}\", expected = {2}")
    @CsvSource({
            "USD, USD, 1",
            "CHF, CHF, 1",
            "ZAR, ZAR, 1",
            "ZAR, USD, 0",
            "USD, CHF, 0",
            "CHF, ZAR, 0",
            "GBP, ZAR, 0"
    })
    public void testIdentityRate(String from, String to, int expected) {
        assertEquals(expected, new Bank().rate(from, to));
    }

    @Test
    public void testMixedAddition() {
        Expression fiveBucks = Money.dollar(STD_AMOUNT);
        Expression tenFrancs = Money.franc(10);
        Bank bank = getBankWithRates();
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
        Expression sum = operation.doAction(money1, money2);
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

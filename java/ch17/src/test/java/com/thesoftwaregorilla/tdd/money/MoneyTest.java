package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;


import java.util.HashMap;
import java.util.function.BiFunction;
import java.util.function.Function;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: (DONE) Add tests to deal with division by zero exceptions in the Money class caused by no exchange rate set.
//TODO: (DONE) Clean up the arithmetic tests align augend and addend with from and to currency pairs.
//TODO: (DONE) Refactor these tests into a nested test structure for the Money class. Basic tests first, then currency arithmetic and conversions.
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

    @Nested
    @DisplayName("construction")
    public class ConstructionTests {

        @DisplayName("constructor")
        @ParameterizedTest(name = "with currency = \"{0}\"")
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

        @DisplayName("toString")
        @ParameterizedTest(name = "with currency = \"{0}\", amount = {1}")
        @CsvSource({
                "USD, 5",
                "CHF, 10",
                "ZAR, 20"
        })
        public void testToString(String currency, int amount) {
            assertEquals(amount + " " + currency, currencyFactories.get(currency).apply(amount).toString());
        }

        @DisplayName("equality")
        @ParameterizedTest(name = "with currency = \"{0}\", unequal currency = \"{1}\", unequal amount = {2}")
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

        @DisplayName("currency")
        @ParameterizedTest(name = "with \"{0}\"")
        @CsvSource ({
                "USD",
                "CHF",
                "ZAR"
        })
        public void testCurrency(String currency) {
            assertEquals(currency, currencyFactories.get(currency).apply(STD_AMOUNT).getCurrency());
        }
    }

    @Nested
    @DisplayName("arithmetic")
    public class ArithmeticTests {


        @Nested
        @DisplayName("multiply")
        public class MultiplicationTests {
            @DisplayName("test for aliasing")
            @ParameterizedTest(name = "with currency = \"{0}\", multiplier = {1},{2}")
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
        }

        @Nested
        @DisplayName("add")
        public class AdditionTests {
            @DisplayName("one currency")
            @ParameterizedTest(name = "with currency = \"{0}\"")
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
        }

        @Nested
        @DisplayName("reduce")
        public class ReductionTests {
            @Test
            @DisplayName("using Money class")
            public void testReduceMoney() {
                Money result = bank.reduce(Money.dollar(1), "USD");
                assertEquals(Money.dollar(1), result);
            }

            @Test
            @DisplayName("using Sum class")
            public void testReduceSum() {
                Expression sum = new Sum(Money.dollar(3), Money.dollar(4));
                Money result = bank.reduce(sum, "USD");
                assertEquals(Money.dollar(7), result);
            }

            @DisplayName("with mixed currency")
            @ParameterizedTest(name = "from \"{0}\", to \"{1}\". Rate = {2}, Amount = {3}")
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
        }

        @Nested
        @DisplayName("mixed currency")
        public class ComplexArithmeticTests {

            @DisplayName("plus with Money")
            @ParameterizedTest(name = "from \"{0}\" {1}, to \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10, USD, 5, 10",
                    "ZAR, 20, CHF, 10, 11",
                    "ZAR, 34, USD, 5, 7",
                    "USD, 5, ZAR, 20, 0"
            })
            public void testMixedAddition(String from, int fromAmt, String to, int toAmt, int expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> add.plus(aug));
            }

            @DisplayName("plus with Sum")
            @ParameterizedTest(name = "from \"{0}\" {1}, to \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10, USD, 5, 15",
                    "ZAR, 20, CHF, 10, 21",
                    "ZAR, 34, USD, 5, 12",
                    "USD, 5, ZAR, 20, 0"
            })
            public void testSumPlusMoney(String from, int fromAmt, String to, int toAmt, int expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> new Sum(aug, add).plus(add));
            }


            @DisplayName("times")
            @ParameterizedTest(name = "from \"{0}\" {1}, to = \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10, USD, 5, 20",
                    "ZAR, 20, CHF, 10, 22",
                    "ZAR, 85, USD, 5, 20",
                    "USD, 5, ZAR, 20, 0"
            })
            public void testSumTimes(String from, int fromAmt, String to, int toAmt, int expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> new Sum(aug, add).times(2));
            }

            private void testReduceHarness(String from, int fromAmt, String to, int toAmt, int expected, BiFunction<Expression, Expression, Expression> operation) {
                Expression money1 = currencyFactories.get(from).apply(fromAmt);
                Expression money2 = currencyFactories.get(to).apply(toAmt);
                Expression sum = operation.apply(money1, money2);
                if (bank.rate(from, to) == 0) {
                    ArithmeticException exception = assertThrows(ArithmeticException.class, () -> {
                        Money result = bank.reduce(sum, to);
                    });
                    assertEquals("Exchange rate not available", exception.getMessage());
                    return;
                }
                Money result = bank.reduce(sum, to);
                assertEquals(currencyFactories.get(to).apply(expected), result);
            }
        }
    }

    @Nested
    @DisplayName("incomplete tests")
    public class IncompleteTests {
        // This test is discussed at the end of Chapter 16, but there is no clean implementation for how to fix make it pass in the book.
        // The test is commented out because it will fail. We'll come back to it later.
        @Test
        @DisplayName("same currency should return Money")
        @Disabled("Disabled until we implement the fix for duplicate plus implementation")
        public void testPlusSameCurrencyReturnsMoney() {
            Expression sum = Money.dollar(1).plus(Money.dollar(1));
            assertInstanceOf(Money.class, sum);
        }

    }

}

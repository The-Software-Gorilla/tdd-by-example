package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.*;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;


import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.HashMap;
import java.util.function.BiFunction;
import java.util.function.Function;

import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//</editor-fold>


@DisplayName("Money Class Tests")
public class MoneyTest {

    private static final BigDecimal STD_AMOUNT = BigDecimal.valueOf(5.00).setScale(2, RoundingMode.HALF_UP);
    private Bank<Money> bank;

    @BeforeEach
    public void beforeEachSetUp() {
        bank = BankTest.getBankWithRates();
    }

    @AfterEach
    public void afterEachTearDown() {
        bank = null;
    }

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
                "USD, 5.00",
                "CHF, 10.00",
                "ZAR, 20.00"
        })
        public void testToString(String currency, BigDecimal amount) {
            assertEquals(amount + " " + currency, currencyFactories.get(currency).apply(amount).toString());
        }

        @DisplayName("equality")
        @ParameterizedTest(name = "with currency = \"{0}\", unequal currency = \"{1}\", unequal amount = {2}")
        @CsvSource({
                "USD, CHF, 6",
                "CHF, ZAR, 7",
                "ZAR, USD, 8"
        })
        public void testEquality(String currency, String unequalCurrency, BigDecimal unequalAmount) {
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
            public void testMultiplication(String currency, BigDecimal multiplier1, BigDecimal multiplier2) {
                Money money = currencyFactories.get(currency).apply(STD_AMOUNT);
                assertEquals(new Money(STD_AMOUNT.multiply(multiplier1), currency), money.times(multiplier1));
                assertEquals(new Money(STD_AMOUNT.multiply(multiplier2), currency), money.times(multiplier2));
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
                assertEquals(new Money(STD_AMOUNT.add(STD_AMOUNT), currency), reduced);
            }
        }

        @Nested
        @DisplayName("reduce")
        public class ReductionTests {
            @Test
            @DisplayName("using Money class")
            public void testReduceMoney() {
                Money result = bank.reduce(Money.dollar(BigDecimal.valueOf(1)), "USD");
                assertEquals(Money.dollar(BigDecimal.valueOf(1)), result);
            }

            @Test
            @DisplayName("using Sum class")
            public void testReduceSum() {
                Expression sum = new Sum(Money.dollar(BigDecimal.valueOf(3)), Money.dollar(BigDecimal.valueOf(4)));
                Money result = bank.reduce(sum, "USD");
                assertEquals(Money.dollar(BigDecimal.valueOf(7)), result);
            }

            @DisplayName("with mixed currency")
            @ParameterizedTest(name = "from \"{0}\", to \"{1}\". Rate = {2}, Amount = {3}")
            @CsvSource({
                    "CHF, USD, 2.00, 2.00",
                    "ZAR, USD, 17.00, 85.00",
                    "ZAR, CHF, 20.00, 60.00"
            })
            public void testReduceMoneyDifferentCurrency(String fromCurrency, String toCurrency, BigDecimal rate, BigDecimal amount) {
                Bank bank = new Bank();
                bank.addRate(fromCurrency, toCurrency, rate);
                Money result = bank.reduce(currencyFactories.get(fromCurrency).apply(amount), toCurrency);
                assertEquals(currencyFactories.get(toCurrency).apply(amount.multiply(rate)), result);
            }
        }

        @Nested
        @DisplayName("mixed currency")
        public class ComplexArithmeticTests {

            @DisplayName("plus with Money")
            @ParameterizedTest(name = "from \"{0}\" {1}, to \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10.00, USD, 5.00, 10.00",
                    "ZAR, 20.00, CHF, 10.00, 11.00",
                    "ZAR, 5.00, USD, 0.00, 0.29",
                    "USD, 1.00, ZAR, 20.00, 37.00",
                    "CHF, 5.00, ZAR, 5.00, 10.00"
            })
            public void testMixedAddition(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> add.plus(aug));
            }

            @DisplayName("plus with Sum")
            @ParameterizedTest(name = "from \"{0}\" {1}, to \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10.00, USD, 5.00, 15.00",
                    "ZAR, 20.00, CHF, 10.00, 21.00",
                    "ZAR, 34.00, USD, 5.00, 12.00",
                    "USD, 1.00, ZAR, 20.00, 57.00",
                    "CHF, 5.00, ZAR, 5.00, 10.00"
            })
            public void testSumPlusMoney(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> new Sum(aug, add).plus(add));
            }


            @DisplayName("times")
            @ParameterizedTest(name = "from \"{0}\" {1}, to = \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10.00, USD, 5.00, 20.00",
                    "ZAR, 20.00, CHF, 10.00, 22.00",
                    "ZAR, 85.00, USD, 5.00, 20.00",
                    "USD, 5.00, ZAR, 20.00, 210.00",
                    "CHF, 5.00, ZAR, 5.00, 10.00"
            })
            public void testSumTimes(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> new Sum(aug, add).times(BigDecimal.valueOf(2)));
            }

            private static void testReduceHarness(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected, BiFunction<Expression, Expression, Expression> operation) {
                Expression money1 = currencyFactories.get(from).apply(fromAmt);
                Expression money2 = currencyFactories.get(to).apply(toAmt);
                Expression sum = operation.apply(money1, money2);
                if (bank.rate(from, to).equals(BigDecimal.ZERO.setScale(8, RoundingMode.HALF_UP))) {
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
    @DisplayName("test not complete in the book")
    public class IncompleteTests {
        @Test
        @DisplayName("same currency should return Money")
        public void testPlusSameCurrencyReturnsMoney() {
            Expression sum = Money.dollar(BigDecimal.valueOf(1)).plus(Money.dollar(BigDecimal.valueOf(1)));
            assertInstanceOf(Money.class, sum);
        }

    }

}

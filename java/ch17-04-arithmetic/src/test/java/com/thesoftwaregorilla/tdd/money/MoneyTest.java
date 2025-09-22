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
// We're done!!!
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
            Money money = Money.from(STD_AMOUNT, currency, bank);
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
            assertEquals(amount + " " + currency, Money.from(amount, currency, bank).toString());
        }

        @DisplayName("equality")
        @ParameterizedTest(name = "with currency = \"{0}\", unequal currency = \"{1}\", unequal amount = {2}")
        @CsvSource({
                "USD, CHF, 6",
                "CHF, ZAR, 7",
                "ZAR, USD, 8"
        })
        public void testEquality(String currency, String unequalCurrency, BigDecimal unequalAmount) {
            Money money = Money.from(STD_AMOUNT, currency, bank);
            assertEquals(money, Money.from(STD_AMOUNT, currency, bank));
            assertNotEquals(money, Money.from(unequalAmount, currency, bank));
            assertNotEquals(money, Money.from(STD_AMOUNT, unequalCurrency, bank));
        }

        @DisplayName("currency")
        @ParameterizedTest(name = "with \"{0}\"")
        @CsvSource ({
                "USD",
                "CHF",
                "ZAR"
        })
        public void testCurrency(String currency) {
            assertEquals(currency, Money.from(STD_AMOUNT, currency, bank).getCurrency());
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
                Money money = Money.from(STD_AMOUNT, currency, bank);
                assertEquals(Money.from(STD_AMOUNT.multiply(multiplier1), currency, bank), money.multiply(multiplier1));
                assertEquals(Money.from(STD_AMOUNT.multiply(multiplier2), currency, bank), money.multiply(multiplier2));
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
                Money money = Money.from(STD_AMOUNT, currency, bank);
                Money sum = money.add(money);
                Money reduced = bank.convert(sum, currency);
                assertEquals(Money.from(STD_AMOUNT.add(STD_AMOUNT), currency, bank), reduced);
            }
        }

        @Nested
        @DisplayName("reduce")
        public class ReductionTests {
            @Test
            @DisplayName("using Money class")
            public void testReduceMoney() {
                Money result = bank.convert(Money.from(BigDecimal.valueOf(1),"USD", bank), "USD");
                assertEquals(Money.from(BigDecimal.valueOf(1), "USD", bank), result);
            }


            @DisplayName("with mixed currency")
            @ParameterizedTest(name = "from \"{0}\", to \"{1}\". Rate = {2}, Amount = {3}")
            @CsvSource({
                    "CHF, USD, 2.00, 2.00",
                    "ZAR, USD, 17.00, 85.00",
                    "ZAR, CHF, 20.00, 60.00"
            })
            public void testReduceMoneyDifferentCurrency(String fromCurrency, String toCurrency, BigDecimal rate, BigDecimal amount) {
                Bank<Money> thisBank = new Bank<Money>();
                thisBank.addRate(fromCurrency, toCurrency, rate);
                Money result = thisBank.convert(Money.from(amount, fromCurrency, thisBank), toCurrency);
                assertEquals(Money.from(amount.multiply(rate), toCurrency, thisBank), result);
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
                    "CHF, 5.00, ZAR, 5.00, 105.00"
            })
            public void testMixedAddition(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> add.add(aug));
            }

            @DisplayName("plus with Sum")
            @ParameterizedTest(name = "from \"{0}\" {1}, to \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10.00, USD, 5.00, 15.00",
                    "ZAR, 20.00, CHF, 10.00, 21.00",
                    "ZAR, 34.00, USD, 5.00, 12.00",
                    "USD, 1.00, ZAR, 20.00, 57.00",
                    "CHF, 5.00, ZAR, 5.00, 110.00"
            })
            public void testSumPlusMoney(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> add.add(aug).add(add));
            }


            @DisplayName("times")
            @ParameterizedTest(name = "from \"{0}\" {1}, to = \"{2}\" {3}, expected = {4}")
            @CsvSource({
                    "CHF, 10.00, USD, 5.00, 20.00",
                    "ZAR, 20.00, CHF, 10.00, 22.00",
                    "ZAR, 85.00, USD, 5.00, 20.00",
                    "USD, 5.00, ZAR, 20.00, 210.00",
                    "CHF, 5.00, ZAR, 5.00, 210.00"
            })
            public void testSumTimes(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected) {
                testReduceHarness(from, fromAmt, to, toAmt, expected,
                        (aug, add) -> add.add(aug).multiply(BigDecimal.valueOf(2)));
            }

            private void testReduceHarness(String from, BigDecimal fromAmt, String to, BigDecimal toAmt, BigDecimal expected, BiFunction<Money, Money, Money> operation) {
                Money money1 = Money.from(fromAmt, from, bank);
                Money money2 = Money.from(toAmt, to, bank);
                if (bank.rate(from, to).equals(BigDecimal.ZERO.setScale(8, RoundingMode.HALF_UP))) {
                    ArithmeticException exception = assertThrows(ArithmeticException.class, () -> {
                        Money sum = operation.apply(money1, money2);
                        Money result = bank.convert(sum, to);
                    });
                    assertEquals("Exchange rate not available", exception.getMessage());
                    return;
                }
                Money sum = operation.apply(money1, money2);
                Money result = bank.convert(sum, to);
                assertEquals(Money.from(expected, to, bank), result);
            }
        }
    }

    @Nested
    @DisplayName("test not complete in the book")
    public class IncompleteTests {
        @Test
        @DisplayName("same currency should return Money")
        public void testPlusSameCurrencyReturnsMoney() {
            Money sum = Money.from(BigDecimal.valueOf(1), "USD", bank).add(Money.from(BigDecimal.valueOf(1), "USD", bank));
            assertInstanceOf(Money.class, sum);
        }

    }

}

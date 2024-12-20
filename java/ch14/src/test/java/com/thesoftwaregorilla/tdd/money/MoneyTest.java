package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class MoneyTest {

    private final Money fiveDollar = Money.dollar(5);

    // This code is not in the book, but my regular tests always have a construction test
    // In chapter 4, when we made amount private, we needed to add a getter in the Dollar class.
    // I could have removed this test because it is tested anyway in the multiplication test, but
    // I like to have a construction test.
    // Also, I wanted to keep parity with the C# code where the Amount member is read only.
    @Test
    public void testConstruction() {
        assertNotNull(fiveDollar);
        assertEquals(5, fiveDollar.getAmount());
        assertEquals("USD", fiveDollar.getCurrency());

        Money fiveFranc = Money.franc(5);
        assertNotNull(fiveFranc);
        assertEquals(5, fiveFranc.getAmount());
        assertEquals("CHF", fiveFranc.getCurrency());

        // Had to add this because I'm South African :)
        Money fiveRand = Money.rand(5);
        assertNotNull(fiveRand);
        assertEquals(5, fiveRand.getAmount());
        assertEquals("ZAR", fiveRand.getCurrency());

    }

    @Test
    public void testEquality() {
        assertTrue(fiveDollar.equals(Money.dollar(5)));
        assertFalse(fiveDollar.equals(Money.dollar(6)));
        assertFalse(Money.franc(5).equals(fiveDollar));
    }


    @Test
    public void testCurrency() {
        assertEquals("USD", Money.dollar(1).getCurrency());
        assertEquals("CHF", Money.franc(1).getCurrency());
        assertEquals("ZAR", Money.rand(1).getCurrency());
    }

    @Test
    public void testMultiplication() {
        assertEquals(Money.dollar(10), fiveDollar.times(2));
        assertEquals(Money.dollar(15), fiveDollar.times(3));
    }

    @Test
    public void testSimpleAddition() {
        Expression sum = fiveDollar.plus(fiveDollar);
        Bank bank = new Bank();
        Money reduced = bank.reduce(sum, "USD");
        assertEquals(Money.dollar(10), reduced);
    }

    @Test
    public void testPlusReturnsSum() {
        Expression result = fiveDollar.plus(fiveDollar);
        Sum sum = (Sum) result;
        assertEquals(fiveDollar, sum.augend);
        assertEquals(fiveDollar, sum.addend);
    }

    @Test
    public void testReduceSum() {
        Expression sum = new Sum(Money.dollar(3), Money.dollar(4));
        Bank bank = new Bank();
        Money result = bank.reduce(sum, "USD");
        assertEquals(Money.dollar(7), result);
    }

    @Test
    public void testReduceMoney() {
        Bank bank = new Bank();
        Money result = bank.reduce(Money.dollar(1), "USD");
        assertEquals(Money.dollar(1), result);
    }

    @Test
    public void testReduceMoneyDifferentCurrency() {
        Bank bank = new Bank();
        bank.addRate("CHF", "USD", 2); // 1 CHF = 0.5 USD
        Money result = bank.reduce(Money.franc(2), "USD");
        assertEquals(Money.dollar(1), result);
        bank.addRate("ZAR", "USD", 17); // 17 ZAR = 1 USD
        Money randExchange = bank.reduce(Money.rand(85), "USD");
        assertEquals(fiveDollar, randExchange);
    }

    // On page 69, Kent suggests trying this to see if the test will pass. In 2002, Java did not have
    // a built-in array equality method test, so the code in the book would not compile, and still fails as a test.
    // The Java code he suggested then is:
    // assertEquals(new Object[] {"abc"}, new Object[] {"abc"});
    // JUnit 5 supports array comparison in 2024, so this test will pass and I am leaving it here for reference.
    @Test
    public void testArrayEquals() {
//        assertEquals(new Object[] {"abc"}, new Object[] {"abc"}); // This test will fail.
        var expected = new Object[] { "abc" };
        var actual = new Object[] { "abc" };
        assertArrayEquals(actual, expected);
    }

    // This code is not in the book, but when I started adding the CurrencyPair class, I realized we should have
    // started with a test.
    @Test
    public void testCurrencyPairEquals() {
        var francToDollar = new CurrencyPair("CHF", "USD");
        assertEquals(francToDollar, new CurrencyPair("CHF", "USD"));
        assertEquals(francToDollar.hashCode(), new CurrencyPair("CHF", "USD").hashCode());
    }

    @Test
    public void testIdentityRate() {
        assertEquals(1, new Bank().rate("USD", "USD"));
        assertEquals(0, new Bank().rate("GBP", "ZAR")); // No rate defined, should return 0
    }

}

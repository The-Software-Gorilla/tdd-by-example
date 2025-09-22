package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: $5 + 10 CHF = $10 if rate is 2:1 - DONE
//TODO: Return Money from $5 + $5
//TODO: Money rounding?
//TODO: Sum.Plus
//TODO: Expression.Times
//TODO: Money Rounding?
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
//</editor-fold>

//<editor-fold desc="DONE List">
//TODO: $5 + $5 = $10 - DONE
//TODO: Bank.Reduce(Money) - DONE
//TODO: Reduce Money with conversion -DONE
//TODO: Reduce (Bank, String) - DONE
//TODO: equals() -DONE
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private - DONE
//TODO: Dollar side-effects? - DONE
//TODO: 5 CHF * 2 = 10 CHF - DONE
//TODO: Dollar/Franc duplication - DONE
//TODO: Common equals - DONE
//TODO: Common Times - DONE
//TODO: Compare Francs with Dollars - DONE
//TODO: Currency? - DONE
//TODO: Delete `testFrancMultiplication()` - DONE
//</editor-fold>

public class MoneyTest {

    private Money fiveDollar;

    @BeforeEach
    public void beforeEachSetUp() {
        fiveDollar = Money.dollar(5);
    }

    @AfterEach
    public void afterEachTearDown() {
        fiveDollar = null;
    }

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

    @Test
    public void testMixedAddition() {
        Expression fiveBucks = fiveDollar;
        Expression tenFrancs = Money.franc(10);
        Bank bank = new Bank();
        bank.addRate("CHF", "USD", 2); // 1 CHF = 0.5 USD
        Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
        assertEquals(Money.dollar(10), result);
    }

}

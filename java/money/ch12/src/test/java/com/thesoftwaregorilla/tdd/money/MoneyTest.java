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

}

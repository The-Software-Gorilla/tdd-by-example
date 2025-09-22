package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private - DONE
//TODO: Dollar side-effects? - DONE
//TODO: Money rounding?
//TODO: equals() -DONE
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
//TODO: 5 CHF * 2 = 10 CHF - DONE
//TODO: Dollar/Franc duplication - DONE
//TODO: Common equals - DONE
//TODO: Common Times - DONE
//TODO: Compare Francs with Dollars - DONE
//TODO: Currency? - DONE
//TODO: Delete `testFrancMultiplication()` - DONE
//</editor-fold>

public class MoneyTest {

    // This code is not in the book, but my regular tests always have a construction test
    // In chapter 4, when we made amount private, we needed to add a getter in the Dollar class.
    // I could have removed this test because it is tested anyway in the multiplication test, but
    // I like to have a construction test.
    // Also, I wanted to keep parity with the C# code where the Amount member is read only.
    @Test
    public void testConstruction() {
        Money fiveDollar = Money.dollar(5);
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
        assertTrue(Money.dollar(5).equals(Money.dollar(5)));
        assertFalse(Money.dollar(5).equals(Money.dollar(6)));
        assertFalse(Money.franc(5).equals(Money.dollar(5)));
    }


    @Test
    public void testCurrency() {
        assertEquals("USD", Money.dollar(1).getCurrency());
        assertEquals("CHF", Money.franc(1).getCurrency());
        assertEquals("ZAR", Money.rand(1).getCurrency());
    }

    @Test
    public void testMultiplication() {
        Money five = Money.dollar(5);
        assertEquals(Money.dollar(10), five.times(2));
        assertEquals(Money.dollar(15), five.times(3));
    }

}

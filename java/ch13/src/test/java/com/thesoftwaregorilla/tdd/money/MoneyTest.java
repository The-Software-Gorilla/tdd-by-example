package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 + $5 = $10
//TODO: Return Money from $5 + $5
//TODO: Bank.Reduce(Money) - DONE
//TODO: Reduce Money with conversion
//TODO: Reduce (Bank, String)
//TODO: Money rounding?
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
//</editor-fold>

//<editor-fold desc="DONE List">
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

}

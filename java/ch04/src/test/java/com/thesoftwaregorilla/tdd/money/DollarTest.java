package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

//<editor-fold desc="TO DO List">
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private
//TODO: Dollar side-effects? - DONE
//TODO: Money rounding?
//TODO: equals() -DONE
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
//</editor-fold>

public class DollarTest {

    // This code is not in the book, but my regular tests always have a construction test
    // In chapter 4, when we made amount private, we needed to add a getter in the Dollar class.
    // I could have removed this test because it is tested anyway in the multiplication test, but
    // I like to have a construction test.
    // Also, I wanted to keep parity with the C# code where the Amount member is read only.
    @Test
    public void testConstruction() {
        Dollar five = new Dollar(5);
        assertNotNull(five);
        assertEquals(5, five.getAmount());
        Dollar ten = new Dollar(10);
        assertNotNull(ten);
        assertEquals(10, ten.getAmount());
    }

    @Test
    public void testMultiplication() {
        Dollar five = new Dollar(5);
        assertEquals(new Dollar(10), five.times(2));
        assertEquals(new Dollar(15), five.times(3));
    }

    @Test
    public void testEquality() {
        Dollar five = new Dollar(5);
        assertEquals(five, new Dollar(5));
        assertNotEquals(five, new Dollar(6));
    }

}

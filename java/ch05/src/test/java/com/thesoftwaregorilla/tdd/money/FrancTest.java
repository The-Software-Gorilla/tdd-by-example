package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;


public class FrancTest {

    @Test
    public void testConstruction() {
        Franc five = new Franc(5);
        assertNotNull(five);
        assertEquals(5, five.getAmount());
        Franc ten = new Franc(10);
        assertNotNull(ten);
        assertEquals(10, ten.getAmount());
    }

    @Test
    public void testMultiplication() {
        Franc five = new Franc(5);
        assertEquals(new Franc(10), five.times(2));
        assertEquals(new Franc(15), five.times(3));
    }

    @Test
    public void testEquality() {
        Franc five = new Franc(5);
        assertEquals(five, new Franc(5));
        assertNotEquals(five, new Franc(6));
    }

}

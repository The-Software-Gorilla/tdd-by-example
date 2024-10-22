package com.thesoftwaregorilla.tdd;

import org.junit.jupiter.api.Test;

import static junit.framework.Assert.*;
import static org.junit.jupiter.api.Assertions.assertNotEquals;

public class DollarTest {

    @Test
    public void testConstruction() {
        Dollar five = new Dollar(5);
        assertEquals(5, five.amount);
        Dollar ten = new Dollar(10);
        assertEquals(10, ten.amount);
    }

    @Test
    public void testMultiplication() {
        Dollar five = new Dollar(5);
        Dollar product = five.times(2);
        assertEquals(10, product.amount);
        product = five.times(3);
        assertEquals(15, product.amount);
    }

    @Test
    public void testEquality() {
        assertEquals(new Dollar(5), new Dollar(5));
        assertNotEquals(new Dollar(5), new Dollar(6));
    }

}

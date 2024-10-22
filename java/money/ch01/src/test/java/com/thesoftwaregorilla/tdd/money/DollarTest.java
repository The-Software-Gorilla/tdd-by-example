package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;

import static junit.framework.Assert.assertEquals;

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
        five.times(2);
        assertEquals(10, five.amount);
    }

}

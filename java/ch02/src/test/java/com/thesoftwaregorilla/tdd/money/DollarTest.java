package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;


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

}

package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class FrancTest {

    @Test
    public void testConstruction() {
        Money five = Money.franc(5);
        assertNotNull(five);
        assertEquals(5, five.getAmount());
        Money ten = Money.franc(10);
        assertNotNull(ten);
        assertEquals(10, ten.getAmount());
    }

    @Test
    public void testMultiplication() {
        Money five = Money.franc(5);
        assertEquals(Money.franc(10), five.times(2));
        assertEquals(Money.franc(15), five.times(3));
    }

}

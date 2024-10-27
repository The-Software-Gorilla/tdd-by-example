package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

@DisplayName("CurrencyPair Tests")
public class CurrencyPairTest {

    @Test
    @DisplayName("equal")
    public void testCurrencyPairEquals() {
        var francToDollar = new CurrencyPair("CHF", "USD");
        assertEquals(francToDollar, new CurrencyPair("CHF", "USD"));
        assertEquals(francToDollar.hashCode(), new CurrencyPair("CHF", "USD").hashCode());
    }

    @Test
    @DisplayName("not equal")
    public void testCurrencyPairNotEquals() {
        var francToDollar = new CurrencyPair("CHF", "USD");
        assertNotEquals(francToDollar, new CurrencyPair("USD", "CHF"));
        assertNotEquals(francToDollar.hashCode(), new CurrencyPair("USD", "CHF").hashCode());
    }

    @Test
    @DisplayName("edge tests for code coverage")
    public void testCurrencyEdgeCases() {
        var francToDollar = new CurrencyPair("CHF", "USD");
        // Test for code coverage
        assertEquals(francToDollar, francToDollar);
        assertNotEquals(francToDollar, "CHF");
    }

}

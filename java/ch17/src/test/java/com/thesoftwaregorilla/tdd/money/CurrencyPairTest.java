package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.*;

@DisplayName("CurrencyPair Tests")
public class CurrencyPairTest {


    @DisplayName("equality tests")
    @ParameterizedTest(name = "from = \"{0}\", to = \"{1}\", equality = {2}")
    @CsvSource({
            "CHF, USD, true",
            "CHF, USD, false",
            "ZAR, USD, true",
            "ZAR, USD, false"
    })
    public void testCurrencyPairs(String from, String to, boolean isEqual) {
        var pair = new CurrencyPair(from, to);
        if(isEqual) {
            assertEquals(pair, new CurrencyPair(from, to));
            assertEquals(pair.hashCode(), new CurrencyPair(from, to).hashCode());
        } else {
            assertNotEquals(pair, new CurrencyPair(to, from));
            assertNotEquals(pair.hashCode(), new CurrencyPair(to, from).hashCode());
        }
    }

    @Test
    @DisplayName("edge tests for code coverage")
    public void testCurrencyEdgeCases() {
        var pair = new CurrencyPair("CHF", "USD");
        // Test for code coverage
        assertNotNull(pair);
        assertEquals(pair, pair);
        assertNotEquals(pair, new Object());
    }

}

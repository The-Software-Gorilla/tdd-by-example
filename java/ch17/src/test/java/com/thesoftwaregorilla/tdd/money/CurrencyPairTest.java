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

    // On page 69, Kent suggests trying this to see if the test will pass. In 2002, Java did not have
    // a built-in array equality method test, so the code in the book would not compile, and still fails as a test.
    // The Java code he suggested then is:
    // assertEquals(new Object[] {"abc"}, new Object[] {"abc"});
    // JUnit 5 supports array comparison in 2024, so this test will pass and I am leaving it here for reference.
    @Test
    @DisplayName("Array Equals test for reference")
    @Disabled("Test exists to illustrate a change to JUnit since the TDD By Example book was published.")
    public void testArrayEquals() {
//        assertEquals(new Object[] {"abc"}, new Object[] {"abc"}); // This test will fail.
        var expected = new Object[] { "abc" };
        var actual = new Object[] { "abc" };
        assertArrayEquals(actual, expected);
    }

}

package com.thesoftwaregorilla.tdd.money;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.math.BigDecimal;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertInstanceOf;

@DisplayName("Operation Class Tests")
public class OperatorTest {

    @BeforeEach
    public void beforeEachSetUp() {
        Money.setDefaultBank(BankTest.getBankWithRates());
    }

    @AfterEach
    public void afterEachTearDown() {
        Money.setDefaultBank(null);
    }

    @Test
    public void testSubtract() {
        Money usd = Money.from(new BigDecimal("5.00"), "USD");
        Money zar = Money.from(new BigDecimal("17.00"), "ZAR");
        Money result = usd.subtract(zar);
        assertInstanceOf(Money.class, result);
        assertEquals(Money.from(new BigDecimal("4.00"), "USD"), result);
    }

    @Test
    public void testAdd() {
        Money usd = Money.from(new BigDecimal("5.00"), "USD");
        Money zar = Money.from(new BigDecimal("17.00"), "ZAR");
        Money result = usd.add(zar);
        assertInstanceOf(Money.class, result);
        assertEquals(Money.from(new BigDecimal("6.00"), "USD"), result);
    }

    @Test
    public void testMultiply() {
        Money usd = Money.from(new BigDecimal("5.00"), "USD");
        Money result = usd.multiply(new BigDecimal("2"));
        assertInstanceOf(Money.class, result);
        assertEquals(Money.from(new BigDecimal("10.00"), "USD"), result);
    }

    @Test
    public void testDivide() {
        Money usd = Money.from(new BigDecimal("5.00"), "USD");
        Money result = usd.divide(new BigDecimal("2"));
        assertInstanceOf(Money.class, result);
        assertEquals(Money.from(new BigDecimal("2.50"), "USD"), result);
    }
}
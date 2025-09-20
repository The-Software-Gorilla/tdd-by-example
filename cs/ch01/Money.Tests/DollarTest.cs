using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class DollarTests
{
    //TODO: $5 + 10 CHF = $10 if rate is 2:1
    //TODO: $5 * 2 = $10 - DONE 
    //TODO: Make "amount" private
    //TODO: Dollar side-effects?
    //TODO: Money rounding?
    
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestConstruction()
    {
        var five = new Dollar(5);
        Assert.That(five, Is.Not.Null);
        Assert.That(five.Amount, Is.EqualTo(5));
        var ten = new Dollar(10);
        Assert.That(ten, Is.Not.Null);
        Assert.That(ten.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestMultiplication()
    {
        Dollar five = new Dollar(5);
        five.Times(2);
        Assert.That(five.Amount, Is.EqualTo(10));
    }
}
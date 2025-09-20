using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class DollarTests
{
    //TODO: $5 + 10 CHF = $10 if rate is 2:1
    //TODO: $5 * 2 = $10 - DONE 
    //TODO: Make "amount" private - DONE
    //TODO: Dollar side-effects? - DONE
    //TODO: Money rounding?
    //TODO: equals() -DONE
    //TODO: hashCode()
    //TODO: Equal null
    //TODO: Equal object
    //TODO: 5 CHF * 2 = 10 CHF - DONE
    //TODO: Dollar/Franc duplication
    //TODO: Common equals
    //TODO: Common Times
    
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestConstruction()
    {
        Dollar five = new Dollar(5);
        Assert.That(five, Is.Not.Null);
        Assert.That(five, Is.InstanceOf<Dollar>());
        Assert.That(five, Is.EqualTo(new Dollar(5)));
    }

    [Test]
    public void TestMultiplication()
    {
        Dollar five = new Dollar(5);
        Assert.That(new Dollar(10), Is.EqualTo(five.Times(2)));
        Assert.That(new Dollar(15), Is.EqualTo(five.Times(3)));
    }

    [Test]
    public void TestEquality()
    {
        Dollar five = new Dollar(5);
        Assert.That(new Dollar(5), Is.EqualTo(five));
        Assert.That(new Dollar(6), Is.Not.EqualTo(five));
    }
}
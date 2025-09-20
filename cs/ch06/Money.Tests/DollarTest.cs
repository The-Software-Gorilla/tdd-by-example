using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class DollarTests
{
    
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

}
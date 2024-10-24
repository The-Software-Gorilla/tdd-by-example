using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class DollarTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConstruction()
    {
        var five = new Dollar(5);
        Assert.IsNotNull(five);
        Assert.That(five.Amount, Is.EqualTo(5));
        var ten = new Dollar(10);
        Assert.IsNotNull(ten);
        Assert.That(ten.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestMultiplication()
    {
        Dollar five = new Dollar(5);
        Assert.That(new Dollar(10), Is.EqualTo(five.Times(2)));
        Assert.That(new Dollar(15), Is.EqualTo(five.Times(3)));
    }

}
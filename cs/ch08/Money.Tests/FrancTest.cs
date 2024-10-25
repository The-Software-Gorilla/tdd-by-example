using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class FrancTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConstruction()
    {
        var five = Money.Franc(5);
        Assert.IsNotNull(five);
        Assert.That(five.Amount, Is.EqualTo(5));
        var ten = Money.Franc(10);
        Assert.IsNotNull(ten);
        Assert.That(ten.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestMultiplication()
    {
        Money five = Money.Franc(5);
        Assert.That(Money.Franc(10), Is.EqualTo(five.Times(2)));
        Assert.That(Money.Franc(15), Is.EqualTo(five.Times(3)));
    }

}
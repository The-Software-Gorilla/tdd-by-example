using NUnit.Framework;
using Money;

namespace Money.Tests;

public class DollarTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConstructor()
    {
        var five = new Dollar(5);
        Assert.IsNotNull(five);
        Assert.That(five.Amount, Is.EqualTo(5));
        var ten = new Dollar(10);
        Assert.IsNotNull(ten);
        Assert.That(ten.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestMultiplation()
    {
        Dollar five = new Dollar(5);
        five.Times(2);
        Assert.That(five.Amount, Is.EqualTo(10));
    }
}
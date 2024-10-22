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
        Dollar product = five.Times(2);
        Assert.That(product.Amount, Is.EqualTo(10));
        product = five.Times(3);
        Assert.That(product.Amount, Is.EqualTo(15));
    }
}
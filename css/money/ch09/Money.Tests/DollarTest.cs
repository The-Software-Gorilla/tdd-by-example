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
    public void TestMultiplication()
    {
        Money five = Money.Dollar(5);
        Assert.That(Money.Dollar(10), Is.EqualTo(five.Times(2)));
        Assert.That(Money.Dollar(15), Is.EqualTo(five.Times(3)));
    }

}
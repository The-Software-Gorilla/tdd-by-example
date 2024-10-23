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
    public void TestMultiplication()
    {
        Money five = Money.Franc(5);
        Assert.That(Money.Franc(10), Is.EqualTo(five.Times(2)));
        Assert.That(Money.Franc(15), Is.EqualTo(five.Times(3)));
    }

}
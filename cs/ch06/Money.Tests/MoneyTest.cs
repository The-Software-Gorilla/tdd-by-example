using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class MoneyTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEquality()
    {
        Assert.That(new Dollar(5), Is.EqualTo(new Dollar(5)));
        Assert.That(new Dollar(6), Is.Not.EqualTo(new Dollar(5)));
        Assert.That(new Franc(5), Is.EqualTo(new Franc(5)));
        Assert.That(new Franc(6), Is.Not.EqualTo(new Franc(5)));
   }

}
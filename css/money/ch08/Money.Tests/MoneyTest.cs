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

        // In NUnit, the following line will not compile because .EqualTo type-checks its argument.
        // I'm commenting the line out to avoid compilation errors, and I added Assert.False below to
        // be more consistent with the book.
        // Assert.That(new Franc(5), Is.Not.EqualTo(new Dollar(5)));
        Assert.False(new Franc(5).Equals(new Dollar(5)));
   }

}
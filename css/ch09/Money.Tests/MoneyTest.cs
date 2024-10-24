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
    public void TestConstruction()
    {
        var fiveDollar = Money.Dollar(5);
        Assert.IsNotNull(fiveDollar);
        Assert.That(fiveDollar.Amount, Is.EqualTo(5));
        Assert.That("USD", Is.EqualTo(fiveDollar.Currency));
        var fiveFranc = Money.Franc(5);
        Assert.IsNotNull(fiveFranc);
        Assert.That(fiveFranc.Amount, Is.EqualTo(5));
        Assert.That("CHF", Is.EqualTo(fiveFranc.Currency));

    }


    [Test]
    public void TestEquality()
    {
        Assert.That(Money.Dollar(5), Is.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Dollar(6), Is.Not.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Franc(5), Is.EqualTo(Money.Franc(5)));
        Assert.That(Money.Franc(6), Is.Not.EqualTo(Money.Franc(5)));

        // In NUnit, the following line will not compile because .EqualTo type-checks its argument.
        // I'm commenting the line out to avoid compilation errors, and I added Assert.False below to
        // be more consistent with the book.
        // Assert.That(Money.Franc(5), Is.Not.EqualTo(Money.Dollar(5)));
        Assert.False(Money.Franc(5).Equals(Money.Dollar(5)));
   }

   [Test]
   public void TestCurrency()
   {
       Assert.That("USD", Is.EqualTo(Money.Dollar(1).Currency));
       Assert.That("CHF", Is.EqualTo(Money.Franc(1).Currency));
   }

}
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

        // Had to add this test because I'm South African! :)
        var fiveRand = Money.Rand(5);
        Assert.IsNotNull(fiveRand);
        Assert.That(fiveRand.Amount, Is.EqualTo(5));
        Assert.That("ZAR", Is.EqualTo(fiveRand.Currency));
    }


    [Test]
    public void TestEquality()
    {
        Assert.That(Money.Dollar(5), Is.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Dollar(6), Is.Not.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Franc(5), Is.Not.EqualTo(Money.Dollar(5)));
   }


   [Test]
   public void TestCurrency()
   {
       Assert.That("USD", Is.EqualTo(Money.Dollar(1).Currency));
       Assert.That("CHF", Is.EqualTo(Money.Franc(1).Currency));
       Assert.That("ZAR", Is.EqualTo(Money.Rand(1).Currency));
   }

    [Test]
    public void TestMultiplication()
    {
        Money fiveDollar = Money.Dollar(5);
        Assert.That(Money.Dollar(10), Is.EqualTo(fiveDollar.Times(2)));
        Assert.That(Money.Dollar(15), Is.EqualTo(fiveDollar.Times(3)));
    }

    [Test]
    public void TestSimpleAddition()
    {
        Money five = Money.Dollar(5);
        Expression sum = five.Plus(five);
        Bank bank = new Bank();
        Money reduced = bank.Reduce(sum, "USD");
        Assert.That(Money.Dollar(10), Is.EqualTo(reduced));
    }


}
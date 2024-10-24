using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

public class MoneyTests
{
    private Money _fiveDollar;

    [SetUp]
    public void Setup()
    {
        _fiveDollar = Money.Dollar(5);
    }

        [Test]
    public void TestConstruction()
    {
        Assert.IsNotNull(_fiveDollar);
        Assert.That(_fiveDollar.Amount, Is.EqualTo(5));
        Assert.That(_fiveDollar.Currency, Is.EqualTo("USD"));

        var fiveFranc = Money.Franc(5);
        Assert.IsNotNull(fiveFranc);
        Assert.That(fiveFranc.Amount, Is.EqualTo(5));
        Assert.That(fiveFranc.Currency, Is.EqualTo("CHF"));

        // Had to add this test because I'm South African! :)
        var fiveRand = Money.Rand(5);
        Assert.IsNotNull(fiveRand);
        Assert.That(fiveRand.Amount, Is.EqualTo(5));
        Assert.That(fiveRand.Currency, Is.EqualTo("ZAR"));
    }


    [Test]
    public void TestEquality()
    {
        Assert.That(_fiveDollar, Is.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Dollar(6), Is.Not.EqualTo(Money.Dollar(5)));
        Assert.That(Money.Franc(5), Is.Not.EqualTo(Money.Dollar(5)));
   }


   [Test]
   public void TestCurrency()
   {
       Assert.That(Money.Dollar(1).Currency, Is.EqualTo("USD"));
       Assert.That(Money.Franc(1).Currency, Is.EqualTo("CHF"));
       Assert.That(Money.Rand(1).Currency, Is.EqualTo("ZAR"));
       
   }

    [Test]
    public void TestMultiplication()
    {
        Assert.That(Money.Dollar(10), Is.EqualTo(_fiveDollar.Times(2)));
        Assert.That(Money.Dollar(15), Is.EqualTo(_fiveDollar.Times(3)));
    }

    [Test]
    public void TestSimpleAddition()
    {
        Expression sum = _fiveDollar.Plus(_fiveDollar);
        Bank bank = new Bank();
        Money reduced = bank.Reduce(sum, "USD");
        Assert.That(Money.Dollar(10), Is.EqualTo(reduced));
    }

    [Test]
    public void TestPlusReturnsSum()
    {
        Expression result = _fiveDollar.Plus(_fiveDollar);
        Sum sum = (Sum) result;
        Assert.That(_fiveDollar, Is.EqualTo(sum.Augend));
        Assert.That(_fiveDollar, Is.EqualTo(sum.Addend));
    }

    [Test]
    public void TestReduceSum()
    {
        Expression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
        Bank bank = new Bank();
        Money result = bank.Reduce(sum, "USD");
        Assert.That(Money.Dollar(7), Is.EqualTo(result));
    }

    [Test]
    public void TestReduceMoney()
    {
        Bank bank = new Bank();
        Money result = bank.Reduce(Money.Dollar(1), "USD");
        Assert.That(Money.Dollar(1), Is.EqualTo(result));
    }

}
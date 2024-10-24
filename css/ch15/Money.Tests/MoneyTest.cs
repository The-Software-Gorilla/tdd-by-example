using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

[TestFixture]
public class MoneyTests
{
    private Money? _fiveDollar;

    [SetUp]
    public void Setup()
    {
        _fiveDollar = Money.Dollar(5);
    }

    [TearDown]
    public void TearDown()
    {
        _fiveDollar = null;
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

    [Test]
    public void TestReduceMoneyDifferentCurrency()
    {
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Money randExchange = bank.Reduce(Money.Franc(2), "USD");
        Assert.That(Money.Dollar(1), Is.EqualTo(randExchange));
        bank.AddRate("ZAR", "USD", 17);
        randExchange = bank.Reduce(Money.Rand(85), "USD");
        Assert.That(randExchange, Is.EqualTo(_fiveDollar));
    }

    // On page 69, Kent suggests trying this to see if the test will pass. In 2002, Java did not have 
    // a built-in array equality method test, so this code would not compile.
    // The Java code he suggested then is:
    // assertEquals(new Object[] {"abc"}, new Object[] {"abc"}); 
    // I included this same test in the Java example, but used assertArrayEquals instead.
    // NUnit supports this in 2024, so this test will pass and I am leaving it here for reference.
    [Test]
    public void TestArrayEquals()
    {
        var actual = new object[] { "abc" };
        Assert.That(actual, Is.EqualTo(new object[] { "abc" }));
    }

    // This code is not in the book, but when I started adding the CurrencyPair class, I realized we should 
    // have started with a test.
    [Test]
    public void TestCurrencyPairEquals()
    {
        var FrancToDollar = new CurrencyPair("CHF", "USD");
        Assert.That(FrancToDollar, Is.EqualTo(new CurrencyPair("CHF", "USD")));
        Assert.That(FrancToDollar.GetHashCode(), Is.EqualTo(new CurrencyPair("CHF", "USD").GetHashCode()));
    }

    [Test]
    public void TestIdentityRate()
    {
        Assert.That(new Bank().Rate("USD", "USD"), Is.EqualTo(1));
        Assert.That(new Bank().Rate("GBP", "ZAR"), Is.EqualTo(0)); // We haven't added this rate to the rate table so make sure it is zero.
    }

    [Test]
    public void TestMixedAddition()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(10)));
    }


}
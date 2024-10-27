using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
//TODO: Move tests into test classes for CurrenctyPair, Bank, and Sum
//TODO: Add tests to deal with the division by zero issue in the Money class.
//TODO: Clean up the arithmetic tests in the MoneyTests class to use from/to instead of augend/addend.
//TODO: Refactor the tests so that they are appropriately grouped into categories like they are nested in the Java code.
//TODO: Create parameterized tests for the MoneyTests class.
//TODO: Get to 100% code coverage.
#endregion
[TestFixture]
[Description("MoneyTest class")]
public class MoneyTest
{
    
    private static readonly Dictionary<String, Func<int, Money>> _currencyFactories = new Dictionary<string, Func<int, Money>>
    {
        { "USD", Money.Dollar },
        { "CHF", Money.Franc },
        { "ZAR", Money.Rand }
    };
    private const int STD_AMT = 5;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        // This method is called only once before any of the tests are run.
    }

    public static Func<int, Money> GetCurrencyFactory(string currency)
    {
        return _currencyFactories[currency];
    }

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

    [TestCase("USD", 5, 5, TestName = "construct USD 5")]
    [TestCase("CHF", 5, 5, TestName = "construct CHF 5")]
    [TestCase("ZAR", 5, 5, TestName = "construct ZAR 5")]
    [Category("construction")]
    public void TestConstruction(string currency, int amount, int expected)
    {
        var money = _currencyFactories[currency].Invoke(amount);
        Assert.IsNotNull(money);
        Assert.That(money.Amount, Is.EqualTo(expected));
        Assert.That(money.Currency, Is.EqualTo(currency));
    }

    [TestCase("USD", 5, TestName = "ToString USD 5")]
    [TestCase("CHF", 5, TestName = "ToString CHF 10")]
    [TestCase("ZAR", 5, TestName = "ToString ZAR 20")]
    [Category("construction")]
    public void TestToString(string currency, int amount)
    {
        var money = _currencyFactories[currency].Invoke(amount);
        Assert.That(money.ToString(), Is.EqualTo(amount + " " + currency));
    }

    [TestCase("USD", "CHF",  6, TestName = "equality USD 6")]
    [TestCase("CHF", "ZAR", 7, TestName = "equality CHF 7")]
    [TestCase("ZAR", "USD", 8, TestName = "equality ZAR 8")]
    [Category("construction")]
    public void TestEquality(string currency1, string currency2, int notEqualAmount)
    {
        var money1 = _currencyFactories[currency1].Invoke(STD_AMT);
        var money2 = _currencyFactories[currency2].Invoke(STD_AMT);
        Assert.That(money1, Is.EqualTo(_currencyFactories[currency1].Invoke(STD_AMT)));
        Assert.That(money1, Is.Not.EqualTo(_currencyFactories[currency1].Invoke(notEqualAmount)));
        Assert.That(money2, Is.Not.EqualTo(money1));
   }

    [TestCase("USD", TestName = "currency USD")]
    [TestCase("CHF", TestName = "currency CHF")]
    [TestCase("ZAR", TestName = "currency ZAR")]
    [Category("construction")]
   public void TestCurrency(string currency)
   {
        var money = _currencyFactories[currency].Invoke(STD_AMT);
        Assert.That(money.Currency, Is.EqualTo(currency));
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

    [Test]
    public void TestSumPlusMoney()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Expression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
        Money result = bank.Reduce(sum, "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(15)));
    }

    [Test]
    public void TestSumTimes()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Expression sum = new Sum(fiveBucks, tenFrancs).Times(2);
        Money result = bank.Reduce(sum, "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(20)));
    }

    // This test is discussed at the end of Chapter 16, but there is no clean implementation for how to fix make it pass in the book.
    // The test is commented out because it will fail. We'll come back to it later.
    [Test]
    [Ignore("Disabled until we implement the fix for duplicate plus implementation")]
    public void TestPlusSameCurrencyReturnsMoney()
    {
        Expression sum = Money.Dollar(1).Plus(Money.Dollar(1));
        Assert.That(sum, Is.InstanceOf<Money>());
    }

}
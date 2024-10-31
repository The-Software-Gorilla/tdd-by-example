using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
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

    [TestCase("USD", 2, 4, TestName = "multiply test for aliasing USD, multiplier  2, 4")]
    [TestCase("CHF", 3, 2, TestName = "multiply test for aliasing CHF, multiplier 3, 2")]
    [TestCase("ZAR", 4, 3, TestName = "multiply test for aliasing ZAR, multiplier 4, 3")]
    [Category("multiplication")]
    public void TestMultiplication(string currency, int multiplier1, int multiplier2)
    {
        var money = _currencyFactories[currency].Invoke(STD_AMT);
        Assert.That(_currencyFactories[currency].Invoke(STD_AMT * multiplier1), Is.EqualTo(money.Times(multiplier1)));
        Assert.That(_currencyFactories[currency].Invoke(STD_AMT * multiplier2), Is.EqualTo(money.Times(multiplier2)));
    }

    [TestCase("USD", TestName = "simple add currency USD")]
    [TestCase("CHF", TestName = "simple add currency CHF")]
    [TestCase("ZAR", TestName = "simple add currency ZAR")]
    [Category("addition")]
    public void TestSimpleAddition(string currency)
    {
        var money = _currencyFactories[currency].Invoke(STD_AMT);
        Expression sum = money.Plus(money);
        Assert.That(sum, Is.InstanceOf<Sum>());
        Money reduced = BankTest.GetBankWithRates().Reduce(sum, currency);
        Assert.That(reduced, Is.EqualTo(_currencyFactories[currency].Invoke(STD_AMT + STD_AMT)));
    }


    [TestCase (TestName = "reduce using Sum class")]
    [Category("reduction")]
    public void TestReduceSum()
    {
        Expression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
        Money result = BankTest.GetBankWithRates().Reduce(sum, "USD");
        Assert.That(Money.Dollar(7), Is.EqualTo(result));
    }

    [TestCase (TestName = "reduce using Money class")]
    [Category("reduction")]
    public void TestReduceMoney()
    {
        Money result = BankTest.GetBankWithRates().Reduce(Money.Dollar(1), "USD");
        Assert.That(Money.Dollar(1), Is.EqualTo(result));
    }

    [TestCase ("CHF", "USD", 2, 2, TestName = "reduce money mixed currency from CHF to USD, rate 2, amount 2")]
    [TestCase ("ZAR", "USD", 17, 85, TestName = "reduce money mixed currency from ZAR to USD, rate 17, amount 85")]
    [TestCase ("ZAR", "CHF", 20, 60, TestName = "reduce money mixed currency from ZAR to CHF, rate 20, amount 60")]
    [Category("reduction")]
    public void TestReduceMoneyDifferentCurrency(string fromCurrency, string toCurrency, int rate, int amount)
    {
        Bank bank = new Bank();
        bank.AddRate(fromCurrency, toCurrency, rate);
        Money result = bank.Reduce(_currencyFactories[fromCurrency].Invoke(amount), toCurrency);
        Assert.That(_currencyFactories[toCurrency].Invoke(amount / rate), Is.EqualTo(result));
    }


    [TestCase ("CHF", 10, "USD", 5, 10, TestName = "plus with Money mixed currency CHF 10 with USD 5, expected 10")]
    [TestCase ("ZAR", 20, "CHF", 10, 11, TestName = "plus with Money mixed currency ZAR 20 with CHF 10, expected 11")]
    [TestCase ("ZAR", 34, "USD", 5, 7, TestName = "plus with Money mixed currency ZAR 34 with USD 5, expected 7")]
    [TestCase ("USD", 5, "ZAR", 20, 0, TestName = "plus with Money missing exchange rate USD 5 with ZAR 20, expected InvalidOperationException")]
    [Category("complex arithmetic")]
    public void TestMixedAddition(string fromCurrency, int fromAmount, string toCurrency, int toAmount, int expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => from.Plus(to));
    }


    [TestCase ("CHF", 10, "USD", 5, 15, TestName = "plus with Sum mixed currency CHF 10 with USD 5, expected 15")]
    [TestCase ("ZAR", 20, "CHF", 10, 21, TestName = "plus with Sum mixed currency ZAR 20 with CHF 10, expected 21")]
    [TestCase ("ZAR", 34, "USD", 5, 12, TestName = "plus with Sum mixed currency ZAR 34 with USD 5, expected 12")]
    [TestCase ("USD", 5, "ZAR", 20, 0, TestName = "plus with Sum missing exchange rate USD 5 with ZAR 20, expected InvalidOperationException")]
    [Category("complex arithmetic")]
    public void TestSumPlusMoney(string fromCurrency, int fromAmount, string toCurrency, int toAmount, int expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => new Sum(from, to).Plus(to));
    }

    [TestCase ("CHF", 10, "USD", 5, 20, TestName = "times with mixed currency CHF 10 with USD 5, expected 20")]
    [TestCase ("ZAR", 20, "CHF", 10, 22, TestName = "times with mixed currency ZAR 20 with CHF 10, expected 22")]
    [TestCase ("ZAR", 85, "USD", 5, 20, TestName = "times with mixed currency ZAR 85 with USD 5, expected 20")]
    [TestCase ("USD", 5, "ZAR", 20, 0, TestName = "times with missing exchange rate USD 5 with ZAR 20, expected InvalidOperationException")]
    [Category("complex arithmetic")]
    public void TestSumTimes(string fromCurrency, int fromAmount, string toCurrency, int toAmount, int expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => new Sum(from, to).Times(2));
    }
    
    private static void testReduceHarness(string from, int fromAmt, string to, int toAmt, int expected, Func<Expression, Expression, Expression> operation)
    {
        Expression fromMoney = _currencyFactories[from].Invoke(fromAmt);
        Expression toMoney = _currencyFactories[to].Invoke(toAmt);
        Expression sum = operation.Invoke(fromMoney, toMoney);
        Bank bank = BankTest.GetBankWithRates();
        if (bank.Rate(from, to) == 0)
        {
            Assert.Throws<InvalidOperationException>(() => bank.Reduce(sum, to));
            return;
        }
        Money result = BankTest.GetBankWithRates().Reduce(sum, to);
        Assert.That(result, Is.EqualTo(_currencyFactories[to].Invoke(expected)));
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
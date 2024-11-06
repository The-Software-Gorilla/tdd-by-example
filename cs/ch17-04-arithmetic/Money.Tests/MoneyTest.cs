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
    
    private const decimal STD_AMT = 5;
    private const string USD = "USD";

    private Bank<Money> _bank;

    [SetUp]
    public void SetUp()
    {
        _bank = BankTest.GetBankWithRates();
    }

    [TearDown]
    public void TearDown()
    {
        _bank = new Bank<Money>();
    }


    [TestCase("USD", 5, 5, TestName = "construct USD 5")]
    [TestCase("CHF", 5, 5, TestName = "construct CHF 5")]
    [TestCase("ZAR", 5, 5, TestName = "construct ZAR 5")]
    [Category("construction")]
    public void TestConstruction(string currency, decimal amount, decimal expected)
    {
        var money =  Money.From(amount, currency, _bank);
        Assert.IsNotNull(money);
        Assert.That(money, Is.InstanceOf<Money>());
        Assert.That(money, Is.InstanceOf<ICurrencyHolder<Money>>());
        Assert.That(money, Is.InstanceOf<IExpression<Money>>());
        Assert.That(money.Amount, Is.EqualTo(expected));
        Assert.That(money.Currency, Is.EqualTo(currency));
    }

    [TestCase("USD", 5, TestName = "ToString USD 5")]
    [TestCase("CHF", 5, TestName = "ToString CHF 10")]
    [TestCase("ZAR", 5, TestName = "ToString ZAR 20")]
    [Category("construction")]
    public void TestToString(string currency, decimal amount)
    {
        var money =  Money.From(amount, currency, _bank);
        Assert.That(money.ToString(), Is.EqualTo(amount + " " + currency));
    }

    [TestCase("USD", "CHF",  6, TestName = "equality USD 6")]
    [TestCase("CHF", "ZAR", 7, TestName = "equality CHF 7")]
    [TestCase("ZAR", "USD", 8, TestName = "equality ZAR 8")]
    [Category("construction")]
    public void TestEquality(string currency1, string currency2, decimal notEqualAmount)
    {
        var money1 =  Money.From(STD_AMT, currency1, _bank);
        var money2 =  Money.From(STD_AMT, currency2, _bank);
        Assert.That(money1, Is.EqualTo( Money.From(STD_AMT, currency1, _bank)));
        Assert.That(money1, Is.Not.EqualTo( Money.From(notEqualAmount, currency1, _bank)));
        Assert.That(money2, Is.Not.EqualTo(money1));
    }

    [TestCase("USD", TestName = "currency USD")]
    [TestCase("CHF", TestName = "currency CHF")]
    [TestCase("ZAR", TestName = "currency ZAR")]
    [Category("construction")]
    public void TestCurrency(string currency)
    {
        var money =  Money.From(STD_AMT, currency, _bank);
        Assert.That(money.Currency, Is.EqualTo(currency));
    }

    [TestCase("USD", 2, 4, TestName = "multiply test for aliasing USD, multiplier  2, 4")]
    [TestCase("CHF", 3, 2, TestName = "multiply test for aliasing CHF, multiplier 3, 2")]
    [TestCase("ZAR", 4, 3, TestName = "multiply test for aliasing ZAR, multiplier 4, 3")]
    [Category("multiplication")]
    public void TestMultiplication(string currency, decimal multiplier1, decimal multiplier2)
    {
        var money =  Money.From(STD_AMT, currency, _bank);
        Assert.That( Money.From(STD_AMT * multiplier1, currency, _bank), Is.EqualTo(money * multiplier1));
        Assert.That( Money.From(STD_AMT * multiplier2, currency, _bank), Is.EqualTo(money * multiplier2));
    }

    [TestCase("USD", TestName = "simple add currency USD")]
    [TestCase("CHF", TestName = "simple add currency CHF")]
    [TestCase("ZAR", TestName = "simple add currency ZAR")]
    [Category("addition")]
    public void TestSimpleAddition(string currency)
    {
        var money =  Money.From(STD_AMT, currency, _bank);
        var sum = money + money;
        Assert.That(sum, Is.InstanceOf<Money>());
        Money reduced = _bank.Convert(sum, currency);
        Assert.That(reduced, Is.EqualTo(Money.From(STD_AMT + STD_AMT, currency, _bank)));
    }

    [Test]
    public void TestSimpleMathReturnsFromCurrency()
    {
        Money usd = Money.From(STD_AMT, USD, _bank);
        Money zar = Money.From(STD_AMT, "ZAR", _bank);
        Assert.That((usd + zar).Currency, Is.EqualTo(USD));
        Assert.That((usd - zar).Currency, Is.EqualTo(USD));
        Assert.That((usd * 2).Currency, Is.EqualTo(USD));
        Assert.That((usd / 2).Currency, Is.EqualTo(USD));
        Assert.That((zar + usd).Currency, Is.EqualTo("ZAR"));
        Assert.That((zar - usd).Currency, Is.EqualTo("ZAR"));
        Assert.That((zar * 2).Currency, Is.EqualTo("ZAR"));
        Assert.That((zar / 2).Currency, Is.EqualTo("ZAR"));
        
    }



    [TestCase (TestName = "reduce using Money class")]
    [Category("reduction")]
    public void TestReduceMoney()
    {
        Money result = _bank.Convert( Money.From(1, USD, _bank), USD);
        Assert.That( Money.From(1, USD, _bank), Is.EqualTo(result));
    }

    [TestCase ("CHF", "USD", 0.5, 2, TestName = "reduce money mixed currency from CHF to USD, rate 0.5, amount 2")]
    [TestCase ("ZAR", "USD", 0.0588235, 85, TestName = "reduce money mixed currency from ZAR to USD, rate 0.0588235, amount 85")]
    [TestCase ("ZAR", "CHF", 0.05, 60, TestName = "reduce money mixed currency from ZAR to CHF, rate 0.05, amount 60")]
    [Category("reduction")]
    public void TestReduceMoneyDifferentCurrency(string fromCurrency, string toCurrency, decimal rate, decimal amount)
    {
        Bank<Money> bank = new Bank<Money>();
        bank.AddRate(fromCurrency, toCurrency, rate);
        Money result = bank.Convert(Money.From(amount, fromCurrency, bank), toCurrency);
        Assert.That(Money.From(Math.Round(amount * rate, 2, MidpointRounding.AwayFromZero), toCurrency, _bank), Is.EqualTo(result));
    }


    [TestCase ("CHF", 10, "USD", 5, 10, TestName = "plus with Money mixed currency CHF 10 with USD 5, expected 10")]
    [TestCase ("ZAR", 20, "CHF", 10, 11, TestName = "plus with Money mixed currency ZAR 20 with CHF 10, expected 11")]
    [TestCase ("ZAR", 34, "USD", 5, 7, TestName = "plus with Money mixed currency ZAR 34 with USD 5, expected 7")]
    [TestCase ("USD", 5, "ZAR", 20, 105.00, TestName = "plus with Money mixed currency USD 5 with ZAR 20, expected 105")]
    [Category("complex arithmetic")]
    public void TestMixedAddition(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => to + from);
    }


    [TestCase ("CHF", 10, "USD", 5, 15, TestName = "plus with Sum mixed currency CHF 10 with USD 5, expected 15")]
    [TestCase ("ZAR", 20, "CHF", 10, 21, TestName = "plus with Sum mixed currency ZAR 20 with CHF 10, expected 21")]
    [TestCase ("ZAR", 34, "USD", 5, 12, TestName = "plus with Sum mixed currency ZAR 34 with USD 5, expected 12")]
    [TestCase ("USD", 5, "ZAR", 20, 125.00, TestName = "plus with Sum mixed currency USD 5 with ZAR 20, expected 105")]
    [Category("complex arithmetic")]
    public void TestSumPlusMoney(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => to + to + from);
    }

    [TestCase ("CHF", 10, "USD", 5, 20, TestName = "times with mixed currency CHF 10 with USD 5, expected 20")]
    [TestCase ("ZAR", 20, "CHF", 10, 22, TestName = "times with mixed currency ZAR 20 with CHF 10, expected 22")]
    [TestCase ("ZAR", 85, "USD", 5, 20, TestName = "times with mixed currency ZAR 85 with USD 5, expected 20")]
    [TestCase ("USD", 5, "ZAR", 20, 210.00, TestName = "times with mixed currency USD 5 with ZAR 20, expected 105.00")]
    [Category("complex arithmetic")]
    public void TestSumTimes(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => (to + from) * 2);
    }
    
    private void testReduceHarness(string from, decimal fromAmt, string to, decimal toAmt, decimal expected, Func<Money, Money, Money> operation)
    {
        var fromMoney = Money.From(fromAmt, from, _bank);
        var toMoney = Money.From(toAmt, to, _bank);
        var sum = operation.Invoke(fromMoney, toMoney);
        if (fromMoney.Bank.Rate(from, to) == 0)
        {
            Assert.Throws<InvalidOperationException>(() => fromMoney.Bank.Convert(sum, to));
            return;
        }
        Money result = _bank.Convert(sum, to);
        Assert.That(result, Is.EqualTo(Money.From(expected, to, _bank)));
    }

    [Test]
    public void TestPlusSameCurrencyReturnsMoney()
    {
        Money sum =  Money.From(1, USD, _bank) + Money.From(1, USD, _bank);
        Assert.That(sum, Is.InstanceOf<Money>());
    }

}
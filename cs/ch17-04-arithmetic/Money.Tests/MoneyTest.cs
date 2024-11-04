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

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        if (Bank.DefaultBank == null || Bank.DefaultBank.RateCount == 0) {
            Bank.DefaultBank = BankTest.GetBankWithRates();
        }
    }


    [TestCase("USD", 5, 5, TestName = "construct USD 5")]
    [TestCase("CHF", 5, 5, TestName = "construct CHF 5")]
    [TestCase("ZAR", 5, 5, TestName = "construct ZAR 5")]
    [Category("construction")]
    public void TestConstruction(string currency, decimal amount, decimal expected)
    {
        var money =  Money.For(amount, currency);
        Assert.IsNotNull(money);
        Assert.That(money.Amount, Is.EqualTo(expected));
        Assert.That(money.Currency, Is.EqualTo(currency));
    }

    [TestCase("USD", 5, TestName = "ToString USD 5")]
    [TestCase("CHF", 5, TestName = "ToString CHF 10")]
    [TestCase("ZAR", 5, TestName = "ToString ZAR 20")]
    [Category("construction")]
    public void TestToString(string currency, decimal amount)
    {
        var money =  Money.For(amount, currency);
        Assert.That(money.ToString(), Is.EqualTo(amount + " " + currency));
    }

    [TestCase("USD", "CHF",  6, TestName = "equality USD 6")]
    [TestCase("CHF", "ZAR", 7, TestName = "equality CHF 7")]
    [TestCase("ZAR", "USD", 8, TestName = "equality ZAR 8")]
    [Category("construction")]
    public void TestEquality(string currency1, string currency2, decimal notEqualAmount)
    {
        var money1 =  Money.For(STD_AMT, currency1);
        var money2 =  Money.For(STD_AMT, currency2);
        Assert.That(money1, Is.EqualTo( Money.For(STD_AMT, currency1)));
        Assert.That(money1, Is.Not.EqualTo( Money.For(notEqualAmount, currency1)));
        Assert.That(money2, Is.Not.EqualTo(money1));
    }

    [TestCase("USD", TestName = "currency USD")]
    [TestCase("CHF", TestName = "currency CHF")]
    [TestCase("ZAR", TestName = "currency ZAR")]
    [Category("construction")]
    public void TestCurrency(string currency)
    {
        var money =  Money.For(STD_AMT, currency);
        Assert.That(money.Currency, Is.EqualTo(currency));
    }

    [TestCase("USD", 2, 4, TestName = "multiply test for aliasing USD, multiplier  2, 4")]
    [TestCase("CHF", 3, 2, TestName = "multiply test for aliasing CHF, multiplier 3, 2")]
    [TestCase("ZAR", 4, 3, TestName = "multiply test for aliasing ZAR, multiplier 4, 3")]
    [Category("multiplication")]
    public void TestMultiplication(string currency, decimal multiplier1, decimal multiplier2)
    {
        var money =  Money.For(STD_AMT, currency);
        Assert.That( Money.For(STD_AMT * multiplier1, currency), Is.EqualTo(money.Times(multiplier1)));
        Assert.That( Money.For(STD_AMT * multiplier2, currency), Is.EqualTo(money.Times(multiplier2)));
    }

    [TestCase("USD", TestName = "simple add currency USD")]
    [TestCase("CHF", TestName = "simple add currency CHF")]
    [TestCase("ZAR", TestName = "simple add currency ZAR")]
    [Category("addition")]
    public void TestSimpleAddition(string currency)
    {
        var money =  Money.For(STD_AMT, currency);
        Expression sum = money.Plus(money);
        Assert.That(sum, Is.InstanceOf<Money>());
        Money reduced = BankTest.GetBankWithRates().Reduce(sum, currency);
        Assert.That(reduced, Is.EqualTo(Money.For(STD_AMT + STD_AMT, currency)));
    }


    [TestCase (TestName = "reduce using Sum class")]
    [Category("reduction")]
    public void TestReduceSum()
    {
        Expression sum = new Sum( Money.For(3, USD),  Money.For(4, USD));
        Money result = BankTest.GetBankWithRates().Reduce(sum, USD);
        Assert.That( Money.For(7, USD), Is.EqualTo(result));
    }

    [TestCase (TestName = "reduce using Money class")]
    [Category("reduction")]
    public void TestReduceMoney()
    {
        Money result = BankTest.GetBankWithRates().Reduce( Money.For(1, USD), USD);
        Assert.That( Money.For(1, USD), Is.EqualTo(result));
    }

    [TestCase ("CHF", "USD", 0.5, 2, TestName = "reduce money mixed currency from CHF to USD, rate 0.5, amount 2")]
    [TestCase ("ZAR", "USD", 0.0588235, 85, TestName = "reduce money mixed currency from ZAR to USD, rate 0.0588235, amount 85")]
    [TestCase ("ZAR", "CHF", 0.05, 60, TestName = "reduce money mixed currency from ZAR to CHF, rate 0.05, amount 60")]
    [Category("reduction")]
    public void TestReduceMoneyDifferentCurrency(string fromCurrency, string toCurrency, decimal rate, decimal amount)
    {
        Bank bank = new Bank();
        bank.AddRate(fromCurrency, toCurrency, rate);
        Money result = bank.Reduce(Money.For(amount, fromCurrency), toCurrency);
        Assert.That(Money.For(Math.Round(amount * rate, 2, MidpointRounding.AwayFromZero), toCurrency), Is.EqualTo(result));
    }


    [TestCase ("CHF", 10, "USD", 5, 10, TestName = "plus with Money mixed currency CHF 10 with USD 5, expected 10")]
    [TestCase ("ZAR", 20, "CHF", 10, 11, TestName = "plus with Money mixed currency ZAR 20 with CHF 10, expected 11")]
    [TestCase ("ZAR", 34, "USD", 5, 7, TestName = "plus with Money mixed currency ZAR 34 with USD 5, expected 7")]
    [TestCase ("USD", 5, "ZAR", 20, 105.00, TestName = "plus with Money mixed currency USD 5 with ZAR 20, expected 105")]
    [Category("complex arithmetic")]
    public void TestMixedAddition(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => from.Plus(to));
    }


    [TestCase ("CHF", 10, "USD", 5, 15, TestName = "plus with Sum mixed currency CHF 10 with USD 5, expected 15")]
    [TestCase ("ZAR", 20, "CHF", 10, 21, TestName = "plus with Sum mixed currency ZAR 20 with CHF 10, expected 21")]
    [TestCase ("ZAR", 34, "USD", 5, 12, TestName = "plus with Sum mixed currency ZAR 34 with USD 5, expected 12")]
    [TestCase ("USD", 5, "ZAR", 20, 125.00, TestName = "plus with Sum miced currency USD 5 with ZAR 20, expected 105")]
    [Category("complex arithmetic")]
    public void TestSumPlusMoney(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => new Sum(from, to).Plus(to));
    }

    [TestCase ("CHF", 10, "USD", 5, 20, TestName = "times with mixed currency CHF 10 with USD 5, expected 20")]
    [TestCase ("ZAR", 20, "CHF", 10, 22, TestName = "times with mixed currency ZAR 20 with CHF 10, expected 22")]
    [TestCase ("ZAR", 85, "USD", 5, 20, TestName = "times with mixed currency ZAR 85 with USD 5, expected 20")]
    [TestCase ("USD", 5, "ZAR", 20, 210.00, TestName = "times with mixed currency USD 5 with ZAR 20, expected 105.00")]
    [Category("complex arithmetic")]
    public void TestSumTimes(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        testReduceHarness(fromCurrency, fromAmount, toCurrency, toAmount, expected, (from, to) => new Sum(from, to).Times(2));
    }
    
    private static void testReduceHarness(string from, decimal fromAmt, string to, decimal toAmt, decimal expected, Func<Expression, Expression, Expression> operation)
    {
        Expression fromMoney = Money.For(fromAmt, from);
        Expression toMoney = Money.For(toAmt, to);
        Expression sum = operation.Invoke(fromMoney, toMoney);
        Bank bank = BankTest.GetBankWithRates();
        if (bank.Rate(from, to) == 0)
        {
            Assert.Throws<InvalidOperationException>(() => bank.Reduce(sum, to));
            return;
        }
        Money result = BankTest.GetBankWithRates().Reduce(sum, to);
        Assert.That(result, Is.EqualTo(Money.For(expected, to)));
    }

    [Test]
    public void TestPlusSameCurrencyReturnsMoney()
    {
        Expression sum =  Money.For(1, USD).Plus( Money.For(1, USD));
        Assert.That(sum, Is.InstanceOf<Money>());
    }

}
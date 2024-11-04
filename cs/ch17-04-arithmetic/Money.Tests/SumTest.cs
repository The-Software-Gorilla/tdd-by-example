using System.Runtime.ConstrainedExecution;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion

[TestFixture]
[Description("Sum Tests")]
public class SumTest
{
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        if (Bank.DefaultBank == null || Bank.DefaultBank.RateCount == 0) {
            Bank.DefaultBank = BankTest.GetBankWithRates();
        }
    }

    [TestCase("USD", 5, "USD", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("CHF", 5, "CHF", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("ZAR", 5, "ZAR", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("CHF", 10, "USD", 5, 10, TestName = "reduce different currency CHF 10 with USD 5, expected 10")]
    [TestCase("ZAR", 20, "CHF", 5, 6, TestName = "reduce different currency ZAR 20 with CHF 5, expected 6")]
    [TestCase("ZAR", 34, "USD", 5, 7, TestName = "reduce different currency ZAR 34 with USD 5, expected 7")]
    [Category("reduction")]
    public void TestReduce(string fromCurrency, decimal fromAmount, string toCurrency, decimal toAmount, decimal expected)
    {
        var sum = new Sum(Money.For(fromAmount, fromCurrency), Money.For(toAmount, toCurrency));
        var result = sum.Reduce(BankTest.GetBankWithRates(), toCurrency);
        Assert.That(result, Is.EqualTo(Money.For(expected, toCurrency)));
    }


    [TestCase("USD", "CHF", 5, TestName = "plus returns sum USD 5")]
    [TestCase("CHF", "CHF", 5, TestName = "plus returns sum CHF 5")]
    [TestCase("ZAR", "USD", 5, TestName = "plus returns sum ZAR 5")]
    [Category("arithmetic")]
    public void TestPlusReturnsSum(string baseCurrency, string targetCurrency, decimal amount)
    {
        var baseMoney = Money.For(amount, baseCurrency);
        var targetMoney = Money.For(amount, targetCurrency);
        var result = baseMoney.Plus(targetMoney);
        if (baseMoney.Currency == targetMoney.Currency)
        {
            Assert.IsInstanceOf<Money>(result);
            Assert.That(result, Is.EqualTo(Money.For(amount + amount, baseCurrency)));
        } 
        else
        { 
            Assert.IsInstanceOf<Sum>(result);
            Sum sum = (Sum) result;
            Assert.That(sum.Augend, Is.EqualTo(baseMoney));
            Assert.That(sum.Addend, Is.EqualTo(targetMoney));
        }
    }

    [TestCase("USD", 5, 4, TestName = "plus with new Sum USD 5, 4")]
    [TestCase("CHF", 7, 9, TestName = "plus with new Sum CHF 7, 9")]
    [TestCase("ZAR", 10, 23, TestName = "plus with new Sum ZAR 10, 23")]
    [Category("arithmetic")]
    public void TestPlus(string currency, decimal amount, decimal extraAmount)
    {
        decimal expected = (amount * 2) + extraAmount;
        testArithmetic(currency, amount, extraAmount, expected, (sum, extraAmount) => sum.Plus(Money.For(extraAmount, currency)));
    }

    [TestCase("USD", 5, 3, TestName = "times with new Sum USD 5")]
    [TestCase("CHF", 7, 5, TestName = "times with new Sum CHF 7")]
    [TestCase("ZAR", 10, 30, TestName = "times with new Sum ZAR 10")]
    [Category("arithmetic")]
    public void TestTimes(string currency, decimal amount, decimal multiplier)
    {
        decimal expected = (amount + amount) * multiplier;
        testArithmetic(currency, amount, multiplier, expected, (sum, extraAmount) => sum.Times(extraAmount));
    }

    private static void testArithmetic(string currency, decimal amount, decimal extraAmount, decimal expectedAmount, Func<Expression, decimal, Expression> operation)
    {
        var money = Money.For(amount, currency);
        Sum sum = new Sum(money, money);
        var result = operation(sum, extraAmount);
        Assert.That(result.Reduce(BankTest.GetBankWithRates(), currency), Is.EqualTo(Money.For(expectedAmount,currency)));
    }
    
}
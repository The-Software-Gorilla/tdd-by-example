namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion

[TestFixture]
[Description("Sum Tests")]
public class SumTest
{
    [TestCase("USD", 5, "USD", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("CHF", 5, "CHF", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("ZAR", 5, "ZAR", 5, 10, TestName = "reduce same currency USD 5")]
    [TestCase("CHF", 10, "USD", 5, 10, TestName = "reduce different currency CHF 10 with USD 5, expected 10")]
    [TestCase("ZAR", 20, "CHF", 5, 6, TestName = "reduce different currency ZAR 20 with CHF 5, expected 6")]
    [TestCase("ZAR", 34, "USD", 5, 7, TestName = "reduce different currency ZAR 34 with USD 5, expected 7")]
    [Category("reduction")]
    public void TestReduce(string fromCurrency, int fromAmount, string toCurrency, int toAmount, int expected)
    {
        var sum = new Sum(MoneyTest.GetCurrencyFactory(fromCurrency).Invoke(fromAmount), MoneyTest.GetCurrencyFactory(toCurrency).Invoke(toAmount));
        var result = sum.Reduce(BankTest.GetBankWithRates(), toCurrency);
        Assert.That(result, Is.EqualTo(MoneyTest.GetCurrencyFactory(toCurrency).Invoke(expected)));
    }


    [TestCase("USD", 5, TestName = "plus returns sum USD 5")]
    [TestCase("CHF", 5, TestName = "plus returns sum CHF 5")]
    [TestCase("ZAR", 5, TestName = "plus returns sum ZAR 5")]
    [Category("arithmetic")]
    public void TestPlusReturnsSum(string currency, int amount)
    {
        var money = MoneyTest.GetCurrencyFactory(currency).Invoke(amount);
        var result = money.Plus(money);
        Assert.That(result, Is.InstanceOf<Sum>());
        Sum sum = (Sum) result;
        Assert.That(sum.Augend, Is.EqualTo(money));
        Assert.That(sum.Addend, Is.EqualTo(money));
    }

    [TestCase("USD", 5, TestName = "plus with new Sum USD 5")]
    [TestCase("CHF", 5, TestName = "plus with new Sum CHF 5")]
    [TestCase("ZAR", 5, TestName = "plus with new Sum ZAR 5")]
    [Category("arithmetic")]
    public void TestPlus(string currency, int amount)
    {
        int expected = amount * 3;
        var money = MoneyTest.GetCurrencyFactory(currency).Invoke(amount);
        var sum = new Sum(money, money);
        var result = sum.Plus(money);
        Assert.That(result, Is.InstanceOf<Sum>());
        Assert.That(result.Reduce(BankTest.GetBankWithRates(), currency), Is.EqualTo(MoneyTest.GetCurrencyFactory(currency).Invoke(expected)));
    }

    [TestCase("USD", 5, 3, TestName = "times with new Sum USD 5")]
    [TestCase("CHF", 7, 5, TestName = "times with new Sum CHF 7")]
    [TestCase("ZAR", 10, 30, TestName = "times with new Sum ZAR 10")]
    [Category("arithmetic")]
    public void TestTimes(string currency, int amount, int multiplier)
    {
        int expected = (amount + amount) * multiplier;
        var money = MoneyTest.GetCurrencyFactory(currency).Invoke(amount);
        var sum = new Sum(money, money);
        var result = sum.Times(multiplier);
        Assert.That(result, Is.InstanceOf<Sum>());
        Assert.That(result.Reduce(BankTest.GetBankWithRates(), currency), Is.EqualTo(MoneyTest.GetCurrencyFactory(currency).Invoke(expected)));
    }    
    
}
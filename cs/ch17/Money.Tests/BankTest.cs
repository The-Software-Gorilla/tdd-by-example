using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion  

[TestFixture]
[Description("BankTest class")]
public class BankTest
{
    private static Bank _bank;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        _bank = GetBankWithRates();
    }

    public static Bank GetBankWithRates()
    {
        if (_bank == null)
        {
            _bank = new Bank();
            _bank.AddRate("CHF", "USD", 2);
            _bank.AddRate("ZAR", "USD", 17);
            _bank.AddRate("ZAR", "CHF", 20);
        }
        return _bank;
    }

    [TestCase("USD", "USD", 1, TestName = "Rate USD to USD returns 1")]
    [TestCase("CHF", "CHF", 1, TestName = "Rate CHF to CHF returns 1")]
    [TestCase("ZAR", "ZAR", 1, TestName = "Rate ZAR to ZAR returns 1")]
    [TestCase("USD", "CHF", 0, TestName = "Rate CHF to USD missing")]
    [TestCase("USD", "ZAR", 0, TestName = "Rate USD to ZAR missing")]
    [TestCase("CHF", "ZAR", 0, TestName = "Rate CHF to ZAR missing")]   
    [TestCase("CHF", "USD", 2, TestName = "Rate USD to CHF returns rate (2)")]
    [TestCase("ZAR", "USD", 17, TestName = "Rate USD to ZAR returns rate (17)")]
    [TestCase("ZAR", "CHF", 20, TestName = "Rate CHF to ZAR returns rate (20)")]
    [Category("rates")]
    public void TestRate(string from, string to, int expected)
    {
        Assert.That(_bank.Rate(from, to), Is.EqualTo(expected));
    }

    [TestCase("CHF", 2, "USD", 1, TestName = "Reduce CHF 2 to USD 1")]
    [TestCase("ZAR", 20, "CHF", 1, TestName = "Reduce ZAR 20 to CHF 1")]
    [TestCase("ZAR", 17, "USD", 1, TestName = "Reduce ZAR 17 to USD 1")]
    [TestCase("USD", 5, "USD", 5, TestName = "Reduce USD 5 to USD 5")]
    [Category("rates")]
    public void TestReduce(string from, int fromAmt, string to, int expected)
    {
        Money result = _bank.Reduce(MoneyTest.GetCurrencyFactory(from).Invoke(fromAmt), to);
        Assert.That(result, Is.EqualTo(MoneyTest.GetCurrencyFactory(to).Invoke(expected)));
    }

    [TestCase("USD", 1, "ZAR", 17, TestName = "Reduce USD 1 to ZAR 17 throws invalid operation exception")]
    [TestCase("CHF", 1, "ZAR", 20, TestName = "Reduce CHF 1 to ZAR 20 throws invalid operation exception")]
    [TestCase("USD", 1, "CHF", 2, TestName = "Reduce USD 1 to CHF 2 throws invalid operation exception")]
    [Category("rates")]
    public void TestReduceDivisionByZero(string from, int fromAmt, string to, int toRate)
    {
        Exception ex = Assert.Throws<InvalidOperationException>(() => _bank.Reduce(MoneyTest.GetCurrencyFactory(from).Invoke(fromAmt), to));
        Assert.That(ex.Message, Is.EqualTo($"No rate found for {from} to {to}"));
    }

    [TestCase("GBP", 1, "ZAR", 20, TestName = "Reduce GBP 1 to ZAR throws key not found exception")]
    [TestCase("INR", 1, "ZAR", 20, TestName = "Reduce INR 1 to CHF throws key not found exception")]
    [TestCase("FRF", 1, "CHF", 0, TestName = "Reduce FRF 1 to CHF throws key not found exception")]
    [Category("rates")]
    public void TestReduceNotInRateTable(string from, int fromAmt, string to, int toRate)
    {
        Exception ex = Assert.Throws<KeyNotFoundException>(() => _bank.Reduce(MoneyTest.GetCurrencyFactory(from).Invoke(fromAmt), to));
        Assert.That(ex.Message, Is.EqualTo($"The given key '{from}' was not present in the dictionary."));
    }

}
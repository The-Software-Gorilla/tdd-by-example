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
    private Bank<Money> _bank;
    [SetUp]
    public void SetUp()
    {
        _bank = GetBankWithRates();
    }

    [TearDown]
    public void TearDown()
    {
        _bank = new Bank<Money>();
    }

    public static Bank<Money> GetBankWithRates()
    {
        Bank<Money> bank = new Bank<Money>();
        bank.AddRate("USD", "CHF", 2);
        bank.AddRate("USD", "ZAR", 17);
        bank.AddRate("CHF", "ZAR", 20);
        return bank;
    }

    [TestCase("USD", "USD", 1, TestName = "Rate USD to USD returns 1")]
    [TestCase("CHF", "CHF", 1, TestName = "Rate CHF to CHF returns 1")]
    [TestCase("ZAR", "ZAR", 1, TestName = "Rate ZAR to ZAR returns 1")]
    [TestCase("CHF", "ZAR", 20, TestName = "Rate CHF to ZAR returns 20")]   
    [TestCase("CHF", "USD", 0.5, TestName = "Rate CHF to USD returns rate 0.5")]
    [TestCase("ZAR", "USD", 0.05882353, TestName = "Rate ZAR to USD returns rate 0.05882353")]
    [TestCase("ZAR", "CHF", 0.05, TestName = "Rate ZAR to CHF returns rate 0.05")]
    [TestCase("USD", "CHF", 2, TestName = "Rate USD to CHF returns rate 2")]
    [TestCase("USD", "ZAR", 17, TestName = "Rate USD to ZAR returns rate 17")]
    [Category("rates")]
    public void TestRate(string from, string to, decimal expected)
    {
        Assert.That(GetBankWithRates().Rate(from, to), Is.EqualTo(expected));
    }

    [TestCase("CHF", 2, "USD", 1, TestName = "Reduce CHF 2 to USD 1")]
    [TestCase("ZAR", 20, "CHF", 1, TestName = "Reduce ZAR 20 to CHF 1")]
    [TestCase("ZAR", 17, "USD", 1, TestName = "Reduce ZAR 17 to USD 1")]
    [TestCase("USD", 5, "USD", 5, TestName = "Reduce USD 5 to USD 5")]
    [Category("rates")]
    public void TestReduce(string from, decimal fromAmt, string to, decimal expected)
    {
        Money result = GetBankWithRates().Convert(Money.From(fromAmt, from, _bank), to);
        Assert.That(result, Is.EqualTo(Money.From(expected, to, _bank)));
    }

    [TestCase("GBP", 1, "ZAR", 20, TestName = "Reduce GBP 1 to ZAR throws key not found exception")]
    [TestCase("INR", 1, "ZAR", 20, TestName = "Reduce INR 1 to CHF throws key not found exception")]
    [TestCase("FRF", 1, "CHF", 0, TestName = "Reduce FRF 1 to CHF throws key not found exception")]
    [Category("rates")]
    public void TestReduceDivisionByZero(string from, decimal fromAmt, string to, decimal toRate)
    {
        Exception ex = Assert.Throws<InvalidOperationException>(() => GetBankWithRates().Convert(Money.From(fromAmt, from, _bank), to));
        Assert.That(ex.Message, Is.EqualTo($"No rate found for {from} to {to}"));
    }

}
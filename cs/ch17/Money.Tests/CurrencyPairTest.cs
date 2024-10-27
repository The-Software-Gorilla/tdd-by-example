using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion

[TestFixture]
[Description("CurrencyPair Tests")]
public class CurrencyPairTest
{

    [TestCase("CHF", "USD", TestName = "CurrencyPair CHF to USD are equal")]
    [TestCase("ZAR", "USD", TestName = "CurrencyPair ZAR to USD are equal")]
    [TestCase("CHF", "USD", false, TestName = "CurrencyPair CHF to USD != USD to CHF")]
    [TestCase("ZAR", "USD", false, TestName = "CurrencyPair ZAR to USD != USD to ZAR")]
    public void TestCurrencyPairs(string from, string to, bool isEqual = true)
    {
        var pair = new CurrencyPair(from, to);
        if(isEqual)
        {
            Assert.That(pair, Is.EqualTo(new CurrencyPair(from, to)));
            Assert.That(pair.GetHashCode(), Is.EqualTo(new CurrencyPair(from, to).GetHashCode()));
        }
        else
        {
            Assert.That(pair, Is.Not.EqualTo(new CurrencyPair(to, from))); // Constructor parameter order intentional to test inequality
            Assert.That(pair.GetHashCode(), Is.Not.EqualTo(new CurrencyPair(to, from).GetHashCode())); // Constructor parameter order intentional to test inequality

        }
    }   

    [TestCase("CHF", "USD", TestName = "CurrencyPair CHF to USD edge case")]
    [TestCase("ZAR", "USD", TestName = "CurrencyPair ZAR to USD edge case")]
    [Description("Edge cases for code coverage")]
    public void TestEdgeCases(string from, string to)
    {
        var pair = new CurrencyPair(from, to);
        Assert.That(pair, Is.Not.EqualTo(null));
        Assert.That(pair, Is.Not.EqualTo(new object()));
        Assert.That(pair, Is.EqualTo(pair));
    }
}
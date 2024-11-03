
namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
//TODO: Add support for subtracting two Money objects
//TODO: Clean up the settle() method in CurrencyTransaction
//TODO: Change the tests to do more assert expected value calculations
//TODO: Add 5 test cases
//TODO: Refactor the tests for separating the asserts
#endregion
[TestFixture]
[Description("CurrencyTransactionTest class")]
public class CurrencyTransactionTest
{
    
    private static Bank _bank;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        _bank = new Bank();
        _bank.AddRate("USD", "ZAR", 17.64m);
        _bank.AddRate("ZAR", "USD", 0.056689m);
        // This method is called only once before any of the tests are run.
    }

    [TestCase]
    public void TestCurrencyTransaction()
    {
        var transaction = new CurrencyTransaction(_bank, Money.For(1000m, "USD"), "ZAR");
        transaction.SourceFee = Money.For(35m, "USD");
        transaction.TargetCurrencyRateFeePercentage = 0.015m; 
        transaction.TargetServiceFee = Money.For(150m, "ZAR");
        transaction.Settle();
        Assert.That(transaction.TargetConversionRate, Is.EqualTo(17.3754m));
        Assert.That(transaction.TargetCurrencyFee.ValueIn("USD", _bank), Is.EqualTo(15.00m));
        Assert.That(transaction.TargetCurrencyFee.ValueIn("ZAR", _bank), Is.EqualTo(264.60m));
        Assert.That(transaction.TargetServiceFee.ValueIn("USD", _bank), Is.EqualTo(8.50m));
        Assert.That(transaction.TargetServiceFee.ValueIn("ZAR", _bank), Is.EqualTo(150.00m));
        Assert.That(transaction.TotalTargetFees.ValueIn("USD", _bank), Is.EqualTo(23.50m));
        Assert.That(transaction.TotalTargetFees.ValueIn("ZAR", _bank), Is.EqualTo(414.60m));
        Assert.That(transaction.SettlementAmount.ValueIn("USD", _bank), Is.EqualTo(976.49m));
        Assert.That(transaction.SettlementAmount.ValueIn("ZAR", _bank), Is.EqualTo(17225.40m));
        Assert.That(transaction.TotalTransactionFees.ValueIn("USD", _bank), Is.EqualTo(58.50m));
        Assert.That(transaction.TotalTransactionFees.ValueIn("ZAR", _bank), Is.EqualTo(1032.00m));
        Assert.That(transaction.TotalTransactionAmount.ValueIn("USD", _bank), Is.EqualTo(1035.00m));
        Assert.That(transaction.TotalTransactionAmount.ValueIn("ZAR", _bank), Is.EqualTo(18257.40m));
    }

    [TestCase]
    public void TestUnsettledTransaction()
    {
        var transaction = new CurrencyTransaction(_bank, Money.For(1000m, "USD"), "ZAR");
        Assert.That(transaction.IsSettled, Is.EqualTo(false));
        Assert.That(transaction.SourceFee, Is.EqualTo( Money.For(0m, "USD")));
        Assert.That(transaction.TargetCurrencyRateFeePercentage, Is.EqualTo(0m)); 
        Assert.That(transaction.TargetServiceFee, Is.EqualTo( Money.For(0m, "ZAR")));
        Assert.That(transaction.TargetConversionRate, Is.EqualTo(0m));
        Assert.That(transaction.TargetCurrencyFee, Is.EqualTo( Money.For(0m, "ZAR")));
        Assert.That(transaction.TotalTargetFees, Is.EqualTo( Money.For(0m, "ZAR")));
        Assert.That(transaction.SettlementAmount, Is.EqualTo( Money.For(0m, "ZAR")));
        Assert.That(transaction.TotalTransactionFees, Is.EqualTo( Money.For(0m, "ZAR")));
        Assert.That(transaction.TotalTransactionAmount, Is.EqualTo( Money.For(0m, "USD")));
    }
}

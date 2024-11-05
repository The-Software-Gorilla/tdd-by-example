
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
    
    // private static Bank bank;
    
    [SetUp]
    public void SetUp()
    {
        Bank bank = new Bank();
        bank.AddRate("USD", "ZAR", 17.64m);
        bank.AddRate("ZAR", "USD", 0.056689m);
        Bank.DefaultBank = bank;
    }

    [TearDown]
    public void TearDown()
    {
        Bank.DefaultBank = new Bank();
    }

    [TestCase]
    public void TestCurrencyTransaction()
    {
        var transaction = new CurrencyTransaction(Money.For(1000m, "USD"), "ZAR");
        transaction.SourceFee = Money.For(35m, "USD");
        transaction.TargetCurrencyRateFeePercentage = 0.015m; 
        transaction.TargetServiceFee = Money.For(150m, "ZAR");
        transaction.Settle();
        Assert.That(transaction.TargetConversionRate, Is.EqualTo(17.3754m));
        Assert.That(transaction.TargetCurrencyFee.AmountIn("USD"), Is.EqualTo(15.00m));
        Assert.That(transaction.TargetCurrencyFee.AmountIn("ZAR"), Is.EqualTo(264.60m));
        Assert.That(transaction.TargetServiceFee.AmountIn("USD"), Is.EqualTo(8.50m));
        Assert.That(transaction.TargetServiceFee.AmountIn("ZAR"), Is.EqualTo(150.00m));
        Assert.That(transaction.TotalTargetFees.AmountIn("USD"), Is.EqualTo(23.50m));
        Assert.That(transaction.TotalTargetFees.AmountIn("ZAR"), Is.EqualTo(414.60m));
        Assert.That(transaction.SettlementAmount.AmountIn("USD"), Is.EqualTo(976.49m));
        Assert.That(transaction.SettlementAmount.AmountIn("ZAR"), Is.EqualTo(17225.40m));
        Assert.That(transaction.TotalTransactionFees.AmountIn("USD"), Is.EqualTo(58.50m));
        Assert.That(transaction.TotalTransactionFees.AmountIn("ZAR"), Is.EqualTo(1032.00m));
        Assert.That(transaction.TotalTransactionAmount.AmountIn("USD"), Is.EqualTo(1035.00m));
        Assert.That(transaction.TotalTransactionAmount.AmountIn("ZAR"), Is.EqualTo(18257.40m));
    }

    [TestCase]
    public void TestUnsettledTransaction()
    {
        var transaction = new CurrencyTransaction(Money.For(1000m, "USD"), "ZAR");
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

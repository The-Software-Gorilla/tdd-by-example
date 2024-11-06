
namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion
[TestFixture]
[Description("CurrencyTransactionTest class")]
public class CurrencyTransactionTest
{
    
    private Bank<Money> _bank;
    
    [SetUp]
    public void SetUp()
    {
        _bank = new Bank<Money>();
        _bank.AddRate("USD", "ZAR", 17.64m);
    }

    [TearDown]
    public void TearDown()
    {
        _bank = new Bank<Money>();
    }

    [TestCase]
    public void TestCurrencyTransaction()
    {
        var transaction = new CurrencyTransaction(Money.From(1000m, "USD", _bank), "ZAR");
        transaction.SourceFee = Money.From(35m, "USD", _bank);
        transaction.TargetCurrencyRateFeePercentage = 0.015m; 
        transaction.TargetServiceFee = Money.From(150m, "ZAR", _bank);
        transaction.Settle();
        Assert.That(transaction.TargetConversionRate, Is.EqualTo(17.3754m));
        Assert.That(transaction.TargetCurrencyFee.AmountIn("USD"), Is.EqualTo(15.00m));
        Assert.That(transaction.TargetCurrencyFee.AmountIn("ZAR"), Is.EqualTo(264.60m));
        Assert.That(transaction.TargetServiceFee.AmountIn("USD"), Is.EqualTo(8.50m));
        Assert.That(transaction.TargetServiceFee.AmountIn("ZAR"), Is.EqualTo(150.00m));
        Assert.That(transaction.TotalTargetFees.AmountIn("USD"), Is.EqualTo(23.50m));
        Assert.That(transaction.TotalTargetFees.AmountIn("ZAR"), Is.EqualTo(414.60m));
        Assert.That(transaction.SettlementAmount.AmountIn("USD"), Is.EqualTo(976.50m));
        Assert.That(transaction.SettlementAmount.AmountIn("ZAR"), Is.EqualTo(17225.40m));
        Assert.That(transaction.TotalTransactionFees.AmountIn("USD"), Is.EqualTo(58.50m));
        Assert.That(transaction.TotalTransactionFees.AmountIn("ZAR"), Is.EqualTo(1032.00m));
        Assert.That(transaction.TotalTransactionAmount.AmountIn("USD"), Is.EqualTo(1035.00m));
        Assert.That(transaction.TotalTransactionAmount.AmountIn("ZAR"), Is.EqualTo(18257.40m));
    }

    [TestCase]
    public void TestUnsettledTransaction()
    {
        var transaction = new CurrencyTransaction(Money.From(1000m, "USD", _bank), "ZAR");
        Assert.That(transaction.IsSettled, Is.EqualTo(false));
        Assert.That(transaction.SourceFee, Is.EqualTo( Money.From(0m, "USD", _bank)));
        Assert.That(transaction.TargetCurrencyRateFeePercentage, Is.EqualTo(0m)); 
        Assert.That(transaction.TargetServiceFee, Is.EqualTo( Money.From(0m, "ZAR", _bank)));
        Assert.That(transaction.TargetConversionRate, Is.EqualTo(0m));
        Assert.That(transaction.TargetCurrencyFee, Is.EqualTo( Money.From(0m, "ZAR", _bank)));
        Assert.That(transaction.TotalTargetFees, Is.EqualTo( Money.From(0m, "ZAR", _bank)));
        Assert.That(transaction.SettlementAmount, Is.EqualTo( Money.From(0m, "ZAR", _bank)));
        Assert.That(transaction.TotalTransactionFees, Is.EqualTo( Money.From(0m, "ZAR", _bank)));
        Assert.That(transaction.TotalTransactionAmount, Is.EqualTo( Money.From(0m, "USD", _bank)));
    }
}

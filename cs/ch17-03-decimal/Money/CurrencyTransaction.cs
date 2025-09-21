namespace TheSoftwareGorilla.TDD.Money;

public class CurrencyTransaction
{
    private Bank Bank { get; }
    public Money SourceAmount { get; }
    public string TargetCurrency { get; }

    public bool IsSettled { get; private set; }

    public CurrencyTransaction(Bank bank, Money sourceAmount, string targetCurrency)
    {
        Bank = bank;
        SourceAmount = sourceAmount;
        TargetCurrency = targetCurrency;
        IsSettled = false;
        SourceFee = new Money(0m, SourceAmount.Currency);
        TargetCurrencyRateFeePercentage = 0m;
        TargetServiceFee = new Money(0m, targetCurrency);
        TargetConversionRate = 0m;
        TargetCurrencyRateFeePercentage = 0m;
        TargetServiceFee = new Money(0m, targetCurrency);
        TargetConversionRate = 0m;
        TargetCurrencyFee = new Money(0m, targetCurrency);
        TotalTargetFees = new Money(0m, targetCurrency);
        SettlementAmount = new Money(0m, targetCurrency);
        TotalTransactionFees = new Money(0m, targetCurrency);
        TotalTransactionAmount = new Money(0m, SourceAmount.Currency);
        TargetAmountAfterRateFee = new Money(0m, targetCurrency);
    }

    public Money SourceFee { get; set; }
    public decimal TargetCurrencyRateFeePercentage { get; set; }
    public Money TargetServiceFee { get; set; }
    public decimal TargetConversionRate { get; private set; }
    public Money TargetCurrencyFee { get; private set; }
    public Money TotalTargetFees { get; private set; }
    public Money SettlementAmount { get; private set; }
    public Money TotalTransactionFees { get; private set; }
    public Money TotalTransactionAmount { get; private set; }
    public Money TargetAmountAfterRateFee { get; private set; }

    public void Settle()
    {
        if (IsSettled) {
            return;
        }
        decimal bankRate = Bank.Rate(SourceAmount.Currency, TargetCurrency);
        TargetConversionRate = Math.Round(bankRate - bankRate * TargetCurrencyRateFeePercentage, 8, MidpointRounding.AwayFromZero);
        TargetAmountAfterRateFee = new Money(SourceAmount.Amount * TargetConversionRate, TargetCurrency);
        Money srcInDest = SourceAmount.Reduce(Bank, TargetCurrency);
        TargetCurrencyFee = new Money(srcInDest.Amount - TargetAmountAfterRateFee.Amount, TargetCurrency);
        TotalTargetFees = TargetCurrencyFee.Plus(TargetServiceFee).Reduce(Bank, TargetCurrency);
        TotalTransactionFees = SourceFee.Plus(TotalTargetFees).Reduce(Bank, TargetCurrency);
        TotalTransactionAmount = SourceAmount.Plus(SourceFee).Reduce(Bank, SourceAmount.Currency);
        SettlementAmount = new Money(TargetAmountAfterRateFee.Amount - TargetServiceFee.Amount, TargetCurrency);
        IsSettled = true;
    }

}
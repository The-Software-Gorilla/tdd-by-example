using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class CurrencyTransaction
{
    public Money SourceAmount { get; }
    public string TargetCurrency { get; }

    public bool IsSettled { get; private set; }

    public CurrencyTransaction(Money sourceAmount, string targetCurrency)
    {
        SourceAmount = sourceAmount;
        TargetCurrency = targetCurrency;
        IsSettled = false;
        SourceFee = Money.From(0m, SourceAmount.Currency);
        TargetCurrencyRateFeePercentage = 0m;
        TargetServiceFee = Money.From(0m, targetCurrency);
        TargetConversionRate = 0m;
        TargetCurrencyRateFeePercentage = 0m;
        TargetServiceFee = Money.From(0m, targetCurrency);
        TargetConversionRate = 0m;
        TargetCurrencyFee = Money.From(0m, targetCurrency);
        TotalTargetFees = Money.From(0m, targetCurrency);
        SettlementAmount = Money.From(0m, targetCurrency);
        TotalTransactionFees = Money.From(0m, targetCurrency);
        TotalTransactionAmount = Money.From(0m, SourceAmount.Currency);
        TargetAmountAfterRateFee = Money.From(0m, targetCurrency);
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
        decimal bankRate = SourceAmount.Bank.Rate(SourceAmount.Currency, TargetCurrency);
        TargetConversionRate = Math.Round(bankRate - bankRate * TargetCurrencyRateFeePercentage, 8, MidpointRounding.AwayFromZero);
        TargetAmountAfterRateFee = Money.From(SourceAmount.Amount * TargetConversionRate, TargetCurrency, SourceAmount.Bank);
        TargetCurrencyFee = Money.From((SourceAmount.Convert(TargetCurrency) - TargetAmountAfterRateFee).Amount, TargetCurrency, SourceAmount.Bank);
        TotalTargetFees = TargetCurrencyFee + TargetServiceFee;
        TotalTransactionFees = SourceFee.Convert(TargetCurrency) + TotalTargetFees;
        TotalTransactionAmount = SourceAmount + SourceFee;
        SettlementAmount = Money.From((TargetAmountAfterRateFee - TargetServiceFee).Amount, TargetCurrency, SourceAmount.Bank);
        IsSettled = true;
    }

}
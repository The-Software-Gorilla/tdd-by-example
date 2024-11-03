using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Money : Expression, ICurrencyHolder<Money>
{
    public static Money For(Decimal amount, String currency)
    {
        return new Money(amount, currency);
    }

    public decimal Amount { get; }

    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    public override Expression Times(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public override Expression Plus(Expression addend)
    {
        if(addend is Money money && Currency == money.Currency)
        {
            return new Money(Amount + money.Amount, Currency);
        }

        return base.Plus(addend);
    }

    public Money Convert(Bank bank, string to)
    {
        decimal rate = bank.Rate(Currency, to);
        if (rate == 0)
        {
            throw new InvalidOperationException($"No rate found for {Currency} to {to}");
        }
        return new Money(Math.Round(Amount * rate, 2, MidpointRounding.AwayFromZero), to);
    }

    public override Money Reduce(Bank bank, string to)
    {
        return this.Convert(bank, to);
    }


    public override bool Equals(object? obj)
    {
        return obj is Money money 
            && Amount == money.Amount 
            && Currency == money.Currency;
    }

    public override string ToString()
    {
        return Amount + " " + Currency;
    }

    public decimal ValueIn(string currency, Bank bank)
    {
        return bank.Reduce(this, currency).Amount;
    }


}
using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Money : Expression
{

    public static Money Dollar(int amount)
    {
        return new Money(amount, "USD");
    }

    public static Money Franc(int amount)
    {
        return new Money(amount, "CHF");
    }

    // Shout out to my South African friends!
    public static Money Rand(int amount)
    {
        return new Money(amount, "ZAR");
    }

    public int Amount { get; }

    public string Currency { get; }

    public Money(int amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Expression Times(int multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public Expression Plus(Expression addend)
    {
        return new Sum(this, addend);
    }

    public Money Reduce(Bank bank, string to)
    {
        int rate = bank.Rate(Currency, to);
        return new Money(Amount / rate, to);
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


}
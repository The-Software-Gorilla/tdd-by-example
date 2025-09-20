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

    internal Money(int amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public virtual Money Times(int multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public Expression Plus(Money addend)
    {
        return new Sum(this, addend);
    }

    public Money Reduce(string to)
    {
        return this;
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
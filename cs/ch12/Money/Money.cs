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

    protected int amount;

    public string Currency { get; }

    private Money(int amount, string currency)
    {
        this.amount = amount;
        Currency = currency;
    }

    public virtual Money Times(int multiplier)
    {
        return new Money(amount * multiplier, Currency);
    }

    public Expression Plus(Money addend)
    {
        return new Money(amount + addend.amount, Currency);
    }

    public override bool Equals(object? obj)
    {
        return obj is Money money 
            && amount == money.amount 
            && Currency == money.Currency;
    }

    public override string ToString()
    {
        return amount + " " + Currency;
    }


}
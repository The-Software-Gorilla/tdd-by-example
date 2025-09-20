namespace TheSoftwareGorilla.TDD.Money;

public class Money
{

    public static Money Dollar(int amount)
    {
        return new Dollar(amount, "USD");
    }

    public static Money Franc(int amount)
    {
        return new Franc(amount, "CHF");
    }

    protected int amount;

    public string Currency { get; }

    protected Money(int amount, string currency)
    {
        this.amount = amount;
        Currency = currency;
    }

    public virtual Money Times(int multiplier)
    {
        return new Money(amount * multiplier, Currency);
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
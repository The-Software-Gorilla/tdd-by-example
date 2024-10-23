namespace TheSoftwareGorilla.TDD.Money;

public abstract class Money
{
    public int Amount { get; }

    public string Currency { get; }

    protected Money(int amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Dollar(int amount)
    {
        return new Dollar(amount, "USD");
    }

    public static Money Franc(int amount)
    {
        return new Franc(amount, "CHF");
    }

    public abstract Money Times(int multiplier);

    public override bool Equals(object? obj)
    {
        return obj is Money money && GetType().Equals(obj.GetType()) && Amount == money.Amount;
    }


}
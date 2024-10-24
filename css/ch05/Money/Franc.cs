namespace TheSoftwareGorilla.TDD.Money;

public class Franc
{
    public int Amount { get; }

    public Franc(int amount)
    {
        Amount = amount;
    }

    public Franc Times(int multiplier)
    {
        return new Franc(Amount * multiplier);
    }

    public override bool Equals(object? obj)
    {
        return obj is Franc franc && Amount == franc.Amount;
    }
}

namespace TheSoftwareGorilla.TDD.Money;

public class Franc
{
    private int amount;

    public Franc(int amount)
    {
        this.amount = amount;
    }

    public Franc Times(int multiplier)
    {
        return new Franc(amount * multiplier);
    }

    public override bool Equals(object? obj)
    {
        return obj is Franc franc && amount == franc.amount;
    }
}

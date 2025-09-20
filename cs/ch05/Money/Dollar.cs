namespace TheSoftwareGorilla.TDD.Money;

public class Dollar
{
    private int amount;

    public Dollar(int amount)
    {
        this.amount = amount;
    }

    public Dollar Times(int multiplier)
    {
        return new Dollar(amount * multiplier);
    }

    public override bool Equals(object? obj)
    {
        return obj is Dollar dollar && amount == dollar.amount;
    }
}

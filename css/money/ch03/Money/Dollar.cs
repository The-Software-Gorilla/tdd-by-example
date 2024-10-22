namespace Money;

public class Dollar
{
    public int Amount { get; }

    public Dollar(int amount)
    {
        Amount = amount;
    }

    public Dollar Times(int multiplier)
    {
        return new Dollar(Amount * multiplier);
    }
}

namespace Money;

public class Dollar
{
    public int Amount { get; set; }

    public Dollar(int amount)
    {
        Amount = amount;
    }

    public void Times(int multiplier)
    {
        Amount *= multiplier;
    }
}

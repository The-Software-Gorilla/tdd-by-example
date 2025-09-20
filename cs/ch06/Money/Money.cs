namespace TheSoftwareGorilla.TDD.Money;

public class Money
{
    protected int amount;

    public override bool Equals(object? obj)
    {
        return obj is Money money && amount == money.amount;
    }


}
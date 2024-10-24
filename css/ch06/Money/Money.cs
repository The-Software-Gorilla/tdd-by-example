namespace TheSoftwareGorilla.TDD.Money;

public class Money
{
    public int Amount { get; protected set; }

    public override bool Equals(object? obj)
    {
        return obj is Money money && Amount == money.Amount;
    }


}
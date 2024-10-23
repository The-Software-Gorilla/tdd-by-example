namespace TheSoftwareGorilla.TDD.Money;

public class Money
{
    public int Amount { get; protected set; }

    public override bool Equals(object? obj)
    {
        return obj is Money money && GetType().Equals(obj.GetType()) && Amount == money.Amount;
    }


}
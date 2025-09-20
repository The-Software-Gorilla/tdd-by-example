namespace TheSoftwareGorilla.TDD.Money;

public abstract class Money
{
    protected int amount;

    public static Money Dollar(int amount)
    {
        return new Dollar(amount);
    }

    public static Money Franc(int amount)
    {
        return new Franc(amount);
    }

    public abstract Money Times(int multiplier);

    public override bool Equals(object? obj)
    {
        return obj is Money money && GetType().Equals(obj.GetType()) && amount == money.amount;
    }


}
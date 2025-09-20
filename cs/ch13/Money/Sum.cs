namespace TheSoftwareGorilla.TDD.Money;

public class Sum : Expression
{
    public Money Augend { get; }
    public Money Addend { get; }

    public Sum(Money augend, Money addend)
    {
        Augend = augend;
        Addend = addend;
    }

    public Money Reduce(string to)
    {
        int amount = Augend.Amount + Addend.Amount;
        return new Money(amount, to);
    }
}
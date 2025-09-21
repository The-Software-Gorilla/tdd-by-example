namespace TheSoftwareGorilla.TDD.Money;

public class Sum : Expression
{
    public Expression Augend { get; }
    public Expression Addend { get; }

    public Sum(Expression augend, Expression addend)
    {
        Augend = augend;
        Addend = addend;
    }

    public override Money Reduce(Bank bank, string to)
    {
        int amount = Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount;
        return new Money(amount, to);
    }


    public override Expression Times(int multiplier)
    {
        return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
    }
}
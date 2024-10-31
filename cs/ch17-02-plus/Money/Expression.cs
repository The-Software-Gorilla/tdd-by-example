using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public abstract class Expression
{
    public abstract Money Reduce(Bank bank, string to);
    public virtual Expression Plus(Expression addend) {
        return new Sum(this, addend);
    }
    public abstract Expression Times(int multiplier);
}
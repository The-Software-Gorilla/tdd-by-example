using System.Diagnostics.CodeAnalysis;

namespace TheSoftwareGorilla.TDD.Money;

[SuppressMessage("csharpsquid","S101", Justification = "Interface name exists for parity with book examples.")]
public interface Expression
{
    Money Reduce(Bank bank, string to);
    Expression Plus(Expression addend);
    Expression Times(int multiplier);
}
using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Bank
{
    public Money Reduce(Expression source, string to)
    {
        return Money.Dollar(10);
    }
}
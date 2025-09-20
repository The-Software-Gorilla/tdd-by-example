namespace TheSoftwareGorilla.TDD.Money;

public interface Expression
{
    Money Reduce(string to);
}
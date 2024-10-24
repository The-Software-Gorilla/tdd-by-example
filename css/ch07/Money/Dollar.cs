namespace TheSoftwareGorilla.TDD.Money;

public class Dollar : Money
{

    public Dollar(int amount)
    {
        Amount = amount;
    }

    public Dollar Times(int multiplier)
    {
        return new Dollar(Amount * multiplier);
    }

}

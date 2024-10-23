namespace TheSoftwareGorilla.TDD.Money;

public class Franc : Money
{

    public Franc(int amount)
    {
        Amount = amount;
    }

    public override Money Times(int multiplier)
    {
        return new Franc(Amount * multiplier);
    }

}

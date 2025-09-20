namespace TheSoftwareGorilla.TDD.Money;

public class Franc : Money
{

    internal Franc(int amount)
    {
        this.amount = amount;
    }

    public override Money Times(int multiplier)
    {
        return new Franc(amount * multiplier);
    }

}

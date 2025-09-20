namespace TheSoftwareGorilla.TDD.Money;

public class Franc : Money
{

    public Franc(int amount)
    {
        this.amount = amount;
    }

    public Franc Times(int multiplier)
    {
        return new Franc(amount * multiplier);
    }

}

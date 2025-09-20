namespace TheSoftwareGorilla.TDD.Money;

public class Dollar : Money
{

    internal Dollar(int amount) 
    {
        this.amount = amount;
    }

    public override Money Times(int multiplier)
    {
        return new Dollar(amount * multiplier);
    }

}

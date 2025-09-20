namespace TheSoftwareGorilla.TDD.Money;

public class Dollar : Money
{

    public Dollar(int amount) 
    {
        this.amount = amount;
    }

    public Dollar Times(int multiplier)
    {
        return new Dollar(amount * multiplier);
    }

}

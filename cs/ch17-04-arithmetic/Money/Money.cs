namespace TheSoftwareGorilla.TDD.Money;

public class Money : ICurrencyHolder<Money>, IExpression<Money>
{

    public static Money From(Decimal amount, String currency, Bank<Money> bank)
    {
        return new Money(amount, currency, bank);
    }

    public decimal Amount { get; }

    public string Currency { get; }

    public Bank<Money> Bank { get; }

    protected Money(decimal amount, string currency, Bank<Money> bank)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
        Bank = bank;
    }

    public virtual Money Add(Money addend)
    {
        Func<Money, Money, Money> plus = (aue, ade) => Money.From(aue.Amount + ade.Amount, aue.Currency, aue.Bank);
        return InvokeOperator(addend, plus);
    }

    public static Money operator +(Money augend, Money addend)
    {
        return augend.Add(addend);
    }

    public virtual Money Subtract(Money subtrahend)
    {
        Func<Money, Money, Money> minus = (minuend, subtrahend) => Money.From(minuend.Amount - subtrahend.Amount, minuend.Currency, minuend.Bank);   
        return InvokeOperator(subtrahend, minus);
    }
    
    public static Money operator -(Money minuend, Money subtrahend)
    {
        return minuend.Subtract(subtrahend);
    }

    public virtual Money Divide(decimal divisor)
    {
        return Money.From(Amount / divisor, Currency, Bank);
    }

    public static Money operator /(Money dividend, decimal divisor)
    {
        return dividend.Divide(divisor);
    }

    public virtual Money Multiply(decimal multiplier)
    {
        return Money.From(Amount * multiplier, Currency, Bank);
    }

    public static Money operator *(Money multiplicand, decimal multiplier)
    {
        return multiplicand.Multiply(multiplier);
    }


    protected virtual Money InvokeOperator(Money other, Func<Money, Money, Money> operation)
    {
        Operation<Money> operationInstance = new Operation<Money>(this, other, Bank, Currency, operation);
        return operationInstance.Apply();
    }

    public virtual Money Convert(string to)
    {
        return Bank.Convert(this, to);
    }


    public override bool Equals(object? obj)
    {
        return obj is Money money 
            && Amount == money.Amount 
            && Currency == money.Currency;
    }

    public static bool operator ==(Money left, Money right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Money left, Money right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString()
    {
        return Amount + " " + Currency;
    }

    public decimal AmountIn(string currency)
    {
        return Convert(currency).Amount;
    }

    public virtual Money NewCurrencyHolder(decimal amount, string currency, Bank<Money> bank)
    {
        return Money.From(amount, currency, bank);
    }
}
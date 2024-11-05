using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Money : ICurrencyHolder<Money>, IExpression<Money>
{
    public static Money For(Decimal amount, String currency)
    {
        return new Money(amount, currency);
    }

    public static Money For(Decimal amount, String currency, Bank<Money> bank)
    {
        return new Money(amount, currency, bank);
    }

    public decimal Amount { get; }

    public string Currency { get; }

    public Bank<Money> Bank { get; } = Bank<Money>.DefaultBank;

    private Money(decimal amount, string currency)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    private Money(decimal amount, string currency, Bank<Money> bank)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
        Bank = bank;
    }

    public Money Add(Money addend)
    {
        Func<Money, Money, Money> plus = (aue, ade) => Money.For(aue.Amount + ade.Amount, aue.Currency, aue.Bank);
        return InvokeOperator(addend, plus);
    }

    public static Money operator +(Money augend, Money addend)
    {
        return augend.Add(addend);
    }

    public Money Subtract(Money subtrahend)
    {
        Func<Money, Money, Money> minus = (minuend, subtrahend) => Money.For(minuend.Amount - subtrahend.Amount, minuend.Currency, minuend.Bank);   
        return InvokeOperator(subtrahend, minus);
    }
    
    public static Money operator -(Money minuend, Money subtrahend)
    {
        return minuend.Subtract(subtrahend);
    }

    public Money Divide(decimal divisor)
    {
        return Money.For(Amount / divisor, Currency, Bank);
    }

    public static Money operator /(Money dividend, decimal divisor)
    {
        return dividend.Divide(divisor);
    }

    public Money Multiply(decimal multiplier)
    {
        return Money.For(Amount * multiplier, Currency, Bank);
    }

    public static Money operator *(Money multiplicand, decimal multiplier)
    {
        return multiplicand.Multiply(multiplier);
    }


    private Money InvokeOperator(Money other, Func<Money, Money, Money> operation)
    {
        Operation<Money> operationInstance = new Operation<Money>(this, other, Bank, Currency, operation);
        return operationInstance.Apply();
    }

    public Money Convert(string to)
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

    public Money NewCurrencyHolder(decimal amount, string currency, Bank<Money> bank)
    {
        return Money.For(amount, currency, bank);
    }
}
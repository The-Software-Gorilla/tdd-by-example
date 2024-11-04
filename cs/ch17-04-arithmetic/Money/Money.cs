using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Money : Expression, ICurrencyHolder<Money>, IExpression<Money>
{
    public static Money For(Decimal amount, String currency)
    {
        return new Money(amount, currency);
    }

    public static Money For(Decimal amount, String currency, Bank bank)
    {
        return new Money(amount, currency, bank);
    }

    public decimal Amount { get; }

    public string Currency { get; }

    public Bank Bank { get; } = Bank.DefaultBank;

    private Money(decimal amount, string currency)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    private Money(decimal amount, string currency, Bank bank)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
        Bank = bank;
    }

    public Money Add(Money addend)
    {
        Func<Money, Money, Money> plus = (augend, addend) => new Money(augend.Amount + addend.Amount, augend.Currency);
        return InvokeOperator(addend, plus);
    }

    public static Money operator +(Money augend, Money addend)
    {
        return augend.Add(addend);
    }

    public Money Subtract(Money subtrahend)
    {
        Func<Money, Money, Money> minus = (minuend, subtrahend) => new Money(minuend.Amount - subtrahend.Amount, minuend.Currency);
        return InvokeOperator(subtrahend, minus);
    }
    
    public static Money operator -(Money minuend, Money subtrahend)
    {
        return minuend.Subtract(subtrahend);
    }

    public Money Divide(decimal divisor)
    {
        return new Money(Amount / divisor, Currency);
    }

    public static Money operator /(Money dividend, decimal divisor)
    {
        return dividend.Divide(divisor);
    }

    public Money Multiply(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
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
        return Convert(this, to);
    }

    public Money Convert(Money money, string to)
    {
        decimal rate = Bank.Rate(Currency, to);
        if (rate == 0)
        {
            throw new InvalidOperationException($"No rate found for {Currency} to {to}");
        }
        return new Money(Math.Round(Amount * rate, 2, MidpointRounding.AwayFromZero), to);
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

    public decimal ValueIn(string currency)
    {
        return Convert(currency).Amount;
    }


#region for deprecation

    public override Money Reduce(Bank bank, string to)
    {
        decimal rate = bank.Rate(Currency, to);
        if (rate == 0)
        {
            throw new InvalidOperationException($"No rate found for {Currency} to {to}");
        }
        return new Money(Math.Round(Amount * rate, 2, MidpointRounding.AwayFromZero), to);
    }

    public override Expression Times(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public override Expression Plus(Expression addend)
    {
        if(addend is Money money && Currency == money.Currency)
        {
            return new Money(Amount + money.Amount, Currency);
        }

        return base.Plus(addend);
    }


#endregion

}
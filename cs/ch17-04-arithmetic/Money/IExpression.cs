namespace TheSoftwareGorilla.TDD.Money;

public interface IExpression<T> where T : ICurrencyHolder<T>
{
    public T Add(Money addend);
    public T Subtract(Money subtrahend);
    public T Multiply(decimal multiplier);
    public T Divide(decimal divisor);
}

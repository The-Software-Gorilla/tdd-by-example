

namespace TheSoftwareGorilla.TDD.Money;

public abstract class Operation<T> : IOperation<T> where T : ICurrencyHolder<T>
{   
    public T Oper1 { get; }
    public T Oper2 { get; }
    public Operation(T oper1, T oper2)
    {
        Oper1 = oper1;
        Oper2 = oper2;
    }

    public abstract T Apply(T expression);
    public virtual T Convert(T converter, Bank bank, string toCurrency) 
    {
        return converter.Convert(bank, toCurrency);
    }
}
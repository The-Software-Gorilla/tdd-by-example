

namespace TheSoftwareGorilla.TDD.Money;

public class Operation<T> : IOperation<T> where T : ICurrencyHolder<T>
{   
    public T Oper1 { get; }
    public T Oper2 { get; }

    public Bank Bank { get; }

    private readonly Func<T, T, T> _calculation;
    private readonly string _toCurrency;


    public Operation(T oper1, T oper2, Bank bank, string toCurrency, Func<T, T, T> calculation)
    {
        Oper1 = oper1;
        Oper2 = oper2;
        Bank = bank;
        _calculation = calculation;
        _toCurrency = toCurrency;
    }

    public virtual T Apply()
    {
        T oper1Converted = Convert(Oper1, _toCurrency);
        T oper2Converted = Convert(Oper2, _toCurrency); 
        return _calculation(oper1Converted, oper2Converted);
    }

    public virtual T Convert(T holder, string to) 
    {
        return holder.Convert(to);
    }
}
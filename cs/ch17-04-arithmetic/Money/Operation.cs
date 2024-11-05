

namespace TheSoftwareGorilla.TDD.Money;

public class Operation<T> : IOperation<T> where T : ICurrencyHolder<T>
{   
    public T LeftOperand { get; }
    public T RightOperand { get; }

    public Bank<T> Bank { get; }

    private readonly Func<T, T, T> _calculation;
    private readonly string _toCurrency;


    public Operation(T leftOperand, T rightOperand, Bank<T> bank, string toCurrency, Func<T, T, T> calculation)
    {
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Bank = bank;
        _calculation = calculation;
        _toCurrency = toCurrency;
    }

    public virtual T Evaluate()
    {
        T left = LeftOperand.Convert(_toCurrency);
        T right = RightOperand.Convert(_toCurrency); 
        return _calculation(left, right);
    }

}
namespace TheSoftwareGorilla.TDD.Money;

public class Subtracter<T> : Operation<T> where T : ICurrencyHolder<T>
{
    private readonly Func<T, T, T> _calculation;
    private readonly IBankProvider _bankProvider;
    public Subtracter(T oper1, T oper2, IBankProvider bankProvider, Func<T, T, T> calculation) : base(oper1, oper2)
    {
        this._calculation = calculation;
    }

    public override T Apply(T expression)
    {
        // T oper1 = Convert(Oper1, expression.Bank, expression.Currency);
        return _calculation.Invoke(Oper1, Oper2);
    }
}

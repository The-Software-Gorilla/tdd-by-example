
namespace TheSoftwareGorilla.TDD.Money;
public interface IOperation<T> : ICurrencyConverter<T> where T : ICurrencyHolder<T>
{
 
    public T Apply(T expression);
}
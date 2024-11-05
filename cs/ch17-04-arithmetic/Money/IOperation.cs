
namespace TheSoftwareGorilla.TDD.Money;
public interface IOperation<T> where T : ICurrencyHolder<T>
{
 
    public T Apply();
}
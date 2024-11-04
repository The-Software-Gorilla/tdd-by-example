
namespace TheSoftwareGorilla.TDD.Money;

public interface ICurrencyConverter<T> where T : ICurrencyHolder<T>
{
    public Bank Bank { get; }
    
    public T Convert(T holder, string to);
}

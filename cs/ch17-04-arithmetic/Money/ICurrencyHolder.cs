using System.Runtime;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money;
public interface ICurrencyHolder<T> : ICurrencyConverter<T> where T : ICurrencyHolder<T>

{
    public decimal Amount { get; }
    public string Currency { get; }
    public T Convert(string to);
    public decimal ValueIn(string currency);
}

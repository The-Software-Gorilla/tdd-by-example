using System.Runtime;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money;
public interface ICurrencyHolder<T> where T : ICurrencyHolder<T>

{
    public Bank Bank { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public T Convert(string to);
    public decimal AmountIn(string currency);
}

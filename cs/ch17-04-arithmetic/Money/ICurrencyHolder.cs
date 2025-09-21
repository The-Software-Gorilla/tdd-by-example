namespace TheSoftwareGorilla.TDD.Money;
public interface ICurrencyHolder<T> where T : ICurrencyHolder<T>

{
    public Bank<T> Bank { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public T Convert(string to);
    public decimal AmountIn(string currency);

    public T NewCurrencyHolder(decimal amount, string currency, Bank<T> bank);
}

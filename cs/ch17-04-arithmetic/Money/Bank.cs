using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Bank<T> : ICurrencyConverter<T> where T : ICurrencyHolder<T>
{
    public static Bank<T> DefaultBank { get; set; } = new Bank<T>();

    private readonly Dictionary<CurrencyPair, decimal> _rates = new Dictionary<CurrencyPair, decimal>();
    public int RateCount => _rates.Count;

    public T Convert(T holder, string to)
    {
        decimal rate = Rate(holder.Currency, to);
        if (rate == 0)
        {
            throw new InvalidOperationException($"No rate found for {holder.Currency} to {to}");
        }
        return holder.NewCurrencyHolder(Math.Round(holder.Amount * rate, 2, MidpointRounding.AwayFromZero), to, this);        
    }

    public decimal Rate(string from, string to)
    {
        if (from.Equals(to))
        {
            return 1;
        }
        decimal rate = _rates.TryGetValue(new CurrencyPair(from, to), out decimal value) ? value : 0;
        return rate;
    }

    public void AddRate(string from, string to, decimal rate)
    {
        _rates.Add(new CurrencyPair(from, to), Math.Round(rate, 8, MidpointRounding.AwayFromZero));
    }

}

    


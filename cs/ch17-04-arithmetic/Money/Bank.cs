using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Bank<T> : ICurrencyConverter<T> where T : ICurrencyHolder<T>
{
    private readonly Dictionary<CurrencyPair, decimal> _rates = new Dictionary<CurrencyPair, decimal>();
    public int RateCount => _rates.Count;

    public T Convert(T holder, string to)
    {
        decimal rate = Rate(holder.Currency, to);
        if (rate == 0)
        {
            throw new InvalidOperationException($"Exchange Rate not found for {holder.Currency} to {to}");
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
        if (!_rates.ContainsKey(new CurrencyPair(from, to)))
        {
            _rates.Add(new CurrencyPair(from, to), Math.Round(rate, 8, MidpointRounding.AwayFromZero));
        } 
        else
        {
            _rates[new CurrencyPair(from, to)] = Math.Round(rate, 8, MidpointRounding.AwayFromZero);
        }
        if (!_rates.ContainsKey(new CurrencyPair(to, from)))
        {
            _rates.Add(new CurrencyPair(to, from), Math.Round(1 / rate, 8, MidpointRounding.AwayFromZero));
        }
    }

}

    


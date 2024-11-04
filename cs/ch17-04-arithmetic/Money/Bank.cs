using System.Net;

namespace TheSoftwareGorilla.TDD.Money;

public class Bank 
{

    public static Bank DefaultBank { get; set; } = new Bank();

    private readonly Dictionary<CurrencyPair, decimal> _rates = new Dictionary<CurrencyPair, decimal>();
    public int RateCount => _rates.Count;
    public Money Reduce(Expression source, string to)
    {
        return source.Reduce(this, to);
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
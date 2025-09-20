namespace TheSoftwareGorilla.TDD.Money;

public class Bank
{
    private Dictionary<CurrencyPair, int> _rates = new Dictionary<CurrencyPair, int>();
    public Money Reduce(Expression source, string to)
    {
        return source.Reduce(this, to);
    }

    public int Rate(string from, string to)
    {
        if (from.Equals(to))
        {
            return 1;
        }
        int rate = _rates.TryGetValue(new CurrencyPair(from, to), out int value) ? value : 0;
        return rate;
    }

    public void AddRate(string from, string to, int rate)
    {
        _rates.Add(new CurrencyPair(from, to), rate);
    }
}
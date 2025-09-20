namespace TheSoftwareGorilla.TDD.Money;

public class CurrencyPair
{
    private readonly string _from;
    private readonly string _to;

    public CurrencyPair(string from, string to)
    {
        _from = from;
        _to = to;
    }

    public override bool Equals(object? obj)
    {
        obj = obj ?? throw new ArgumentNullException(nameof(obj));
        if (obj.GetType() != typeof(CurrencyPair)) return false;
        CurrencyPair pair = (CurrencyPair)obj;
        return _from.Equals(pair._from) && _to.Equals(pair._to);
    }

    // The book uses zero for a hashCode. Objects.hash() was only added to Java in Java 7. 
    // C# has HashCode.Combine() which is the equivalent of Objects.hash() in Java.
    public override int GetHashCode()
    {
        return HashCode.Combine(_from, _to);
    }
}
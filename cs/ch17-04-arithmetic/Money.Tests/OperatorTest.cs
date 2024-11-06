namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion

[TestFixture]
[Description("Operator Tests")]
public class OperatorTest
{

    private Bank<Money> _bank;
    [SetUp]
    public void SetUp()
    {
        _bank = BankTest.GetBankWithRates();
    }

    [TearDown]
    public void TearDown()
    {
        _bank = new Bank<Money>();
    }

    [Test]
    public void TestSubtract() {
        Money usd = Money.From(5, "USD", _bank);
        Money zar = Money.From(17, "ZAR", _bank);
        Money result = usd.Subtract(zar);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.From(4, "USD", _bank)));
        Assert.That(result, Is.EqualTo(usd - zar));
    }

    [Test]
    public void TestAdd() {
        Money usd = Money.From(5, "USD", _bank);
        Money zar = Money.From(17, "ZAR", _bank);
        Money result = usd.Add(zar);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.From(6, "USD", _bank)));
        Assert.That(result, Is.EqualTo(usd + zar));

    }

    [Test]
    public void TestMultiply() {
        Money usd = Money.From(5, "USD", _bank);
        Money result = usd.Multiply(2);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.From(10, "USD", _bank)));
        Assert.That(result, Is.EqualTo(usd * 2));
    }

    [Test]
    public void TestDivide() {
        Money usd = Money.From(5, "USD", _bank);
        Money result = usd.Divide(2);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.From(2.5m, "USD", _bank)));
        Assert.That(result, Is.EqualTo(usd / 2));
    }

    [Test]
    public void TestEqualityOperators() {
        Money usd = Money.From(5, "USD", _bank);
        Money zar = Money.From(5, "ZAR", _bank);
        Assert.True(usd == Money.From(5, "USD", _bank));
        Assert.False(usd == Money.From(4, "USD", _bank));
        Assert.False(zar == usd);
        Assert.True(usd != zar);
        Assert.True(usd != Money.From(4, "USD", _bank));
        Assert.False(usd != Money.From(5, "USD", _bank));
    }
    
}

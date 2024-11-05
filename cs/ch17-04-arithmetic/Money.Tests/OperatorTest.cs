namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TO DO's
#endregion

[TestFixture]
[Description("Operator Tests")]
public class OperatorTest
{
    [SetUp]
    public void SetUp()
    {
        Bank<Money>.DefaultBank = BankTest.GetBankWithRates();
    }

    [TearDown]
    public void TearDown()
    {
        Bank<Money>.DefaultBank = new Bank<Money>();
    }

    [Test]
    public void TestSubtract() {
        Money usd = Money.For(5, "USD");
        Money zar = Money.For(17, "ZAR");
        Money result = usd.Subtract(zar);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.For(4, "USD")));
        Assert.That(result, Is.EqualTo(usd - zar));
    }

    [Test]
    public void TestAdd() {
        Money usd = Money.For(5, "USD");
        Money zar = Money.For(17, "ZAR");
        Money result = usd.Add(zar);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.For(6, "USD")));
        Assert.That(result, Is.EqualTo(usd + zar));

    }

    [Test]
    public void TestMultiply() {
        Money usd = Money.For(5, "USD");
        Money result = usd.Multiply(2);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.For(10, "USD")));
        Assert.That(result, Is.EqualTo(usd * 2));
    }

    [Test]
    public void TestDivide() {
        Money usd = Money.For(5, "USD");
        Money result = usd.Divide(2);
        Assert.IsInstanceOf<Money>(result);
        Assert.That(result, Is.EqualTo(Money.For(2.5m, "USD")));
        Assert.That(result, Is.EqualTo(usd / 2));
    }

    [Test]
    public void TestEqualityOperators() {
        Money usd = Money.For(5, "USD");
        Money zar = Money.For(5, "ZAR");
        Assert.True(usd == Money.For(5, "USD"));
        Assert.False(usd == Money.For(4, "USD"));
        Assert.False(zar == usd);
        Assert.True(usd != zar);
        Assert.True(usd != Money.For(4, "USD"));
        Assert.False(usd != Money.For(5, "USD"));
    }
    
}

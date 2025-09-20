namespace TheSoftwareGorilla.TDD.Money.Tests;

public class FrancTests
{
    
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestConstruction()
    {
        Money five = Money.Franc(5);
        Assert.That(five, Is.Not.Null);
        Assert.That(five, Is.InstanceOf<Franc>());
        Assert.That(five, Is.EqualTo(Money.Franc(5)));
    }

    [Test]
    public void TestMultiplication()
    {
        Money five = Money.Franc(5);
        Assert.That(Money.Franc(10), Is.EqualTo(five.Times(2)));
        Assert.That(Money.Franc(15), Is.EqualTo(five.Times(3)));
    }

}
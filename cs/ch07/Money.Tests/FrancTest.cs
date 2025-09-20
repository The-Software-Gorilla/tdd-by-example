namespace TheSoftwareGorilla.TDD.Money.Tests;

public class FrancTests
{
    //TODO: 5 CHF * 2 = 10 CHF - DONE
    
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestConstruction()
    {
        var five = new Franc(5);
        Assert.That(five, Is.Not.Null);
        Assert.That(five, Is.InstanceOf<Franc>());
        Assert.That(five, Is.EqualTo(new Franc(5)));
    }

    [Test]
    public void TestMultiplication()
    {
        Franc five = new Franc(5);
        Assert.That(new Franc(10), Is.EqualTo(five.Times(2)));
        Assert.That(new Franc(15), Is.EqualTo(five.Times(3)));
    }

}
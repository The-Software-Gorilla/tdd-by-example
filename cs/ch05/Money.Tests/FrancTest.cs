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
        Franc five = new Franc(5);
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

    [Test]
    public void TestEquality()
    {
        Franc five = new Franc(5);
        Assert.That(new Franc(5), Is.EqualTo(five));
        Assert.That(new Franc(6), Is.Not.EqualTo(five));
    }
}
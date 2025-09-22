using NUnit.Framework;
using TheSoftwareGorilla.TDD.Money;

namespace TheSoftwareGorilla.TDD.Money.Tests;


#region TODO List
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private
//TODO: Dollar side-effects? - DONE
//TODO: Money rounding?
//TODO: equals() -DONE
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
#endregion

public class DollarTests
{
    
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestConstruction()
    {
        Dollar five = new Dollar(5);
        Assert.That(five, Is.Not.Null);
        Assert.That(five.Amount, Is.EqualTo(5));
        var ten = new Dollar(10);
        Assert.That(ten, Is.Not.Null);
        Assert.That(ten.Amount, Is.EqualTo(10));
    }

    [Test]
    public void TestMultiplication()
    {
        Dollar five = new Dollar(5);
        Dollar product = five.Times(2);
        Assert.That(product.Amount, Is.EqualTo(10));
        product = five.Times(3);
        Assert.That(product.Amount, Is.EqualTo(15));
    }

    [Test]
    public void TestEquality()
    {
        Assert.That(new Dollar(5), Is.EqualTo(new Dollar(5)));
        Assert.That(new Dollar(5), Is.Not.EqualTo(new Dollar(6)));
    }
}
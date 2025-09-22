namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TODO List
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private - DONE
//TODO: Dollar side-effects? - DONE
//TODO: Money rounding?
//TODO: equals() -DONE
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
//TODO: 5 CHF * 2 = 10 CHF - DONE
//TODO: Dollar/Franc duplication
//TODO: Common equals - DONE
//TODO: Common Times
//TODO: Compare Francs with Dollars
#endregion

public class MoneyTests
{
    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

    [Test]
    public void TestEquality()
    {
        // In NUnit, the lines that follow these variable declarations will not compile if we inline the constructor  
        // calls (e.g., Assert.That(new Dollar(5), Is.EqualTo(new Dollar(5))); because Is.EqualTo type-checks
        // its argument.
        Dollar fived = new Dollar(5);
        Dollar sixd = new Dollar(6);
        Franc fivef = new Franc(5);
        Franc sixf = new Franc(6);
        
        Assert.That(fived, Is.EqualTo(new Dollar(5)));
        Assert.That(sixd, Is.Not.EqualTo(new Dollar(5)));
        Assert.That(fivef, Is.EqualTo(new Franc(5)));
        Assert.That(sixf, Is.Not.EqualTo(new Franc(5)));
   }

}
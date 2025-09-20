namespace TheSoftwareGorilla.TDD.Money.Tests;

public class MoneyTests
{
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
    //TODO: Compare Francs with Dollars - DONE
    //TODO: Currency? - DONE
    //TODO: Delete `testFrancMultiplication()`

    [SetUp]
    public void Setup()
    {
        // No setup required for these tests
    }

	[Test]
    public void TestConstruction()
    {
       	Money fiveDollar = Money.Dollar(5);
        Assert.That(fiveDollar, Is.Not.Null);
        Assert.That(fiveDollar, Is.InstanceOf<Dollar>());
        Assert.That(fiveDollar, Is.EqualTo(Money.Dollar(5)));
        Assert.That(fiveDollar.Currency, Is.EqualTo("USD"));
        Money fiveFranc = Money.Franc(5);
        Assert.That(fiveFranc, Is.Not.Null);
        Assert.That(fiveFranc, Is.InstanceOf<Franc>());
        Assert.That(fiveFranc, Is.EqualTo(Money.Franc(5)));
        Assert.That(fiveFranc.Currency, Is.EqualTo("CHF"));
    }


    [Test]
    public void TestEquality()
    {
        // In NUnit, the lines that follow these variable declarations will not compile if we inline the factory method 
        // calls (e.g., Assert.That(Money.Dollar(5), Is.EqualTo(Money.Dollar(5))); because Is.EqualTo type-checks
        // its argument.
        Money fived = Money.Dollar(5);
        Money sixd = Money.Dollar(6);
        Money fivef = Money.Franc(5);
        Money sixf = Money.Franc(6);
        
        Assert.That(fived, Is.EqualTo(Money.Dollar(5)));
        Assert.That(sixd, Is.Not.EqualTo(Money.Dollar(5)));
        Assert.That(fivef, Is.EqualTo(Money.Franc(5)));
        Assert.That(sixf, Is.Not.EqualTo(Money.Franc(5)));		
		
        // In NUnit, the following line will not compile because .EqualTo type-checks its argument.
        // I'm commenting the line out to avoid compilation errors, and I added Assert(Is.False) below to
        // be more consistent with the book.
        // Assert.That(new Franc(5), Is.Not.EqualTo(new Dollar(5)));
        Assert.That(Money.Franc(5).Equals(Money.Dollar(5)), Is.False);
	}

   [Test]
   public void TestCurrency()
   {
       Assert.That(Money.Dollar(1).Currency, Is.EqualTo("USD"));
       Assert.That(Money.Franc(1).Currency, Is.EqualTo("CHF"));
   }

}
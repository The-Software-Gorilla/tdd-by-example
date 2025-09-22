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
//TODO: Dollar/Franc duplication - DONE
//TODO: Common equals - DONE
//TODO: Common Times - DONE
//TODO: Compare Francs with Dollars - DONE
//TODO: Currency? - DONE
//TODO: Delete `testFrancMultiplication()` - DONE
#endregion

public class MoneyTests
{
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
        Assert.That(fiveDollar, Is.InstanceOf<Money>());
        Assert.That(fiveDollar, Is.EqualTo(Money.Dollar(5)));
        Assert.That(fiveDollar.Currency, Is.EqualTo("USD"));
        
        Money fiveFranc = Money.Franc(5);
        Assert.That(fiveFranc, Is.Not.Null);
        Assert.That(fiveFranc, Is.InstanceOf<Money>());
        Assert.That(fiveFranc, Is.EqualTo(Money.Franc(5)));
        Assert.That(fiveFranc.Currency, Is.EqualTo("CHF"));
		
		// Had to add this test because I'm South African! :)
        Money fiveRand = Money.Rand(5);
        Assert.That(fiveRand, Is.Not.Null);
        Assert.That(fiveRand, Is.InstanceOf<Money>());
        Assert.That(fiveRand, Is.EqualTo(Money.Rand(5)));
        Assert.That(fiveRand.Currency, Is.EqualTo("ZAR"));

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
       Assert.That(Money.Rand(1).Currency, Is.EqualTo("ZAR"));
   }

    [Test]
    public void TestMultiplication()
    {
        Money fiveDollar = Money.Dollar(5);
        Assert.That(fiveDollar.Times(2), Is.EqualTo(Money.Dollar(10)));
        Assert.That(fiveDollar.Times(3), Is.EqualTo(Money.Dollar(15)));
    }


}
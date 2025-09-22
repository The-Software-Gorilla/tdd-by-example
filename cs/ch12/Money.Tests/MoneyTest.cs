namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TODO List
//TODO: $5 + 10 CHF = $10 if rate is 2:1
//TODO: $5 + $5 = $10
//TODO: Money rounding?
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
#endregion

#region DONE List
//TODO: equals() -DONE
//TODO: $5 * 2 = $10 - DONE 
//TODO: Make "amount" private - DONE
//TODO: Dollar side-effects? - DONE
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

    private Money _fiveDollar;

    [SetUp]
    public void Setup()
    {
        _fiveDollar = Money.Dollar(5);
    }

        [Test]
    public void TestConstruction()
    {
        Assert.That(_fiveDollar, Is.Not.Null);
        Assert.That(_fiveDollar, Is.InstanceOf<Money>());
        Assert.That(_fiveDollar, Is.EqualTo(Money.Dollar(5)));
        Assert.That(_fiveDollar.Currency, Is.EqualTo("USD"));
        
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
        Assert.That(_fiveDollar, Is.EqualTo(Money.Dollar(5)));
        Assert.That(_fiveDollar, Is.Not.EqualTo(Money.Dollar(6)));
        Assert.That(_fiveDollar, Is.Not.EqualTo(Money.Franc(5)));
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
        Assert.That(_fiveDollar.Times(2), Is.EqualTo(Money.Dollar(10)));
        Assert.That(_fiveDollar.Times(3), Is.EqualTo(Money.Dollar(15)));
    }

    [Test]
    public void TestSimpleAddition()
    {
        Expression sum = _fiveDollar.Plus(_fiveDollar);
        Bank bank = new Bank();
        Money reduced = bank.Reduce(sum, "USD");
        Assert.That(reduced, Is.EqualTo(Money.Dollar(10)));
    }
}
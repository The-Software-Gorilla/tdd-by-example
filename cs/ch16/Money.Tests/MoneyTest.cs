namespace TheSoftwareGorilla.TDD.Money.Tests;

#region TODO List
//TODO: Return Money from $5 + $5 - No good way to do this in the book.
//TODO: Sum.Plus - DONE
//TODO: Expression.Times - DONE
//TODO: Money Rounding?
//TODO: hashCode()
//TODO: Equal null
//TODO: Equal object
#endregion

#region DONE List
//TODO: $5 + 10 CHF = $10 if rate is 2:1 - DONE
//TODO: $5 + $5 = $10 - DONE
//TODO: Bank.Reduce(Money) - DONE
//TODO: Reduce Money with conversion -DONE
//TODO: Reduce (Bank, String) - DONE
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

[TestFixture]
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

    [Test]
    public void TestPlusReturnsSum()
    {
        Expression result = _fiveDollar.Plus(_fiveDollar);
        Sum sum = (Sum) result;
        Assert.That(_fiveDollar, Is.EqualTo(sum.Augend));
        Assert.That(_fiveDollar, Is.EqualTo(sum.Addend));
    }

    [Test]
    public void TestReduceSum()
    {
        Expression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
        Bank bank = new Bank();
        Money result = bank.Reduce(sum, "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(7)));
    }

    [Test]
    public void TestReduceMoney()
    {
        Bank bank = new Bank();
        Money result = bank.Reduce(Money.Dollar(1), "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(1)));
    }

    [Test]
    public void TestReduceMoneyDifferentCurrency()
    {
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Money francExchange = bank.Reduce(Money.Franc(2), "USD");
        Assert.That(francExchange, Is.EqualTo(Money.Dollar(1)));
        bank.AddRate("ZAR", "USD", 17);
        Money randExchange = bank.Reduce(Money.Rand(85), "USD");
        Assert.That(randExchange, Is.EqualTo(_fiveDollar));
    }

    // On page 69, Kent suggests trying this to see if the test will pass. In 2002, Java did not have 
    // a built-in array equality method test, so this code would not compile.
    // The Java code he suggested then is:
    // assertEquals(new Object[] {"abc"}, new Object[] {"abc"}); 
    // I included this same test in the Java example, but used assertArrayEquals instead.
    // NUnit supports this in 2024, so this test will pass and I am leaving it here for reference.
    [Test]
    public void TestArrayEquals()
    {
        var actual = new object[] { "abc" };
        Assert.That(actual, Is.EqualTo(new object[] { "abc" }));
    }

    // This code is not in the book, but when I started adding the CurrencyPair class, I realized we should 
    // have started with a test.
    [Test]
    public void TestCurrencyPairEquals()
    {
        var FrancToDollar = new CurrencyPair("CHF", "USD");
        Assert.That(FrancToDollar, Is.EqualTo(new CurrencyPair("CHF", "USD")));
        Assert.That(FrancToDollar.GetHashCode(), Is.EqualTo(new CurrencyPair("CHF", "USD").GetHashCode()));
    }

    [Test]
    public void TestIdentityRate()
    {
        Assert.That(new Bank().Rate("USD", "USD"), Is.EqualTo(1));
        Assert.That(new Bank().Rate("GBP", "ZAR"), Is.EqualTo(0)); // We haven't added this rate to the rate table so make sure it is zero.
    }

    [Test]
    public void TestMixedAddition()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(10)));
    }

    [Test]
    public void TestSumPlusMoney()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Expression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
        Money result = bank.Reduce(sum, "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(15)));
    }

    [Test]
    public void TestSumTimes()
    {
        Expression fiveBucks = _fiveDollar;
        Expression tenFrancs = Money.Franc(10);
        Bank bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        Expression sum = new Sum(fiveBucks, tenFrancs).Times(2);
        Money result = bank.Reduce(sum, "USD");
        Assert.That(result, Is.EqualTo(Money.Dollar(20)));
    }

    // This test is discussed at the end of Chapter 16, but there is no clean implementation for how to fix make it pass in the book.
    // The test is commented out because it will fail. We'll come back to it later.
    // [Test]
    // public void TestPlusSameCurrencyReturnsMoney()
    // {
    //     Expression sum = Money.Dollar(1).Plus(Money.Dollar(1));
    //     Assert.That(sum, Is.InstanceOf<Money>());
    // }

}
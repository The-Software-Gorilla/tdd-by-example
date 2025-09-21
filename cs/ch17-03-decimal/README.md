# Test-Driven Development - C# - Chapter 17 Part 3

In Chapter 17 of the book ["Test-Driven Development By Example" by Kent Beck](https://a.co/d/1sr05eT), Kent writes a retrospective on the
development of the example application in the previous chapters. He tasks the reader with going back and doing a few
things:
- Make the tests more comprehensive - Done in ch17-01
- Fix the "Return `Money` from \$5 + \$5" TODO item - Done in ch17-02
- Review the design decisions made

This solution is the third part of a four-part series that addresses these tasks. This part focuses on converting
the `Money.Amount` property to a `decimal` and building the `CurrencyTransaction`.

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 17 - Part 3 - Switching to `decimal` Amounts
Once I completed the action items at the beginning of Chapter 17, there was one item left I had to fix - Money rounding.
This was the point at which I really needed to fix the decimal issue, too. 

I started thinking about a very real-world problem and how it applies to this example -
how I send money back to family in South Africa. It's a foreign wire transfer and there are
a number of charges associated with the transaction on both sides. I thought it would be good
to try and come up with a way to calculate the total cost of the transaction, itemizing all the 
charges. 

There were some problems with the initial implementation the book provides:
1. In the real world, the currency and exchange rates are always decimals and the initial 
implementation used integers. I needed to change the contained data type of money and rates 
to decimal.
2. I needed to subtract some of the charges from the original transaction amount. The initial 
implementation does not have support for subtraction.
3. The rate tables work backwards to what I expected. That's because the `reduce()` method
in the Money class divides the amount by the rate rather than multiplying it. I changed this
to multiply the amount by the rate, but that meant changing the rate tables in the test too.

### Decimals 
I dealt with the first problem by changing the data type of the transaction to `BigDecimal` in Java, 
but C# has a much more elegant implementation - `decimal`. Syntactically it's an intrinsic data type
like `int`, `long`, `float`, etc. This was still a very intrusive change that impacted virtually every 
part of the code, but given we had working unit tests, it was literally just a search and replace of
`int` to `decimal`, which was much easier than the changes in the Java code.

I did end up changing some of the unit tests so that I could validate the arithmetic with `decimal` values,
but that was more additional testing rather than broken tests because of the data type change, which 
is what I ended up with for the Java code.

### Subtraction
The second problem was a little more difficult and I needed to have real examples to work with before
I figured out how to solve it, so I deferred solving the subtraction issue until I had real examples 
of CurrencyTransactions to work with. I ended up solving the subtraction issue in the 
CurrencyTransaction class, which is not where it should be solved. This code will be refactored in ch17-04.

Every time I work on this code in C#, I can't help but think I need to make operator overloading work 
for the Money object so that `new Money(20, "USD") + new Money(85, "ZAR")` returns 
a `new Money(25, "USD")`. IOW, give me a Money object in the same currency as the first operand in
an operator override. We'll get there.

### ValueIn
One of the things I love with C# is that properties are just part of the language. Java needs getter and 
setters that make it a function call to get the value of a property. As I was writing the tests for 
CurrencyTransaction, I realized it would be really nice to have a money object value in a specific currency
without calling the Reduce function to do it. So I added an `ValueIn()` method that takes the target currency
for the conversion and the bank object and returns a decimal value with the amount. Just a neat helper 
to make the code more readable. This also made me refactor the Java code to make it more readable.

### Currency Transactions
I created a new class called CurrencyTransaction to perform the calculations for the 
transaction. I created a new test class called CurrencyTransactionTest that tests the
CurrencyTransaction class.

The way a currency transaction works is that you call the bank, tell them how much you want to send,
and pay a transaction fee to send it. The bank then sends the money to the foreign bank. The foreign
bank then deducts a percentage of the money based on the exchange rate, and a service fee to transfer
the money to the destination account.

The transaction I am using as an example is from the US (USD) to South Africa (ZAR). South Africa's
currency is the Rand, symbol R. The exchange rate when I wrote this was ZAR 17.64:USD 1 or 
USD 0.056689:ZAR 1. 

Here's an example of a currency transaction:
- You want to send $1000 to South Africa
- The transaction fee is $35
- The foreign bank charges a 1.5% fee on the exchange rate. 17.64 * 0.015 = R0.2646. That means the 
net exchange rate is 17.64 - 0.2646 = 17.3754
- The foreign bank charges a service fee of ZAR 150 for routing the transaction to the destination 
account.
- The fees for the transaction are therefore:
  - Source Transaction Fee, normally paid on top of the amount transferred: $35
  - Exchange Rate Fee: R264.60 or $15.00 calculated as follows: 
    - Source: $1000 * 17.64 = R17,640
    - Rate Fee Rate: 17.64 * (100 - 1.5%) = 17.3754 
    - Amount calculated against Rate Fee Rate: Source ($1000) * Rate Fee Rate (17.3754) = R17,375.40
    - Rate Fee: Source - Amount calculated against Rate Fee Rate = R264.60 or $15.00
  - Foreign Transaction Service Fee: ZAR 150 or $8.50
  - Total Foreign Fees: R264.60 + R150 = R414.60 or $23.50
  - Total Fees: \$35 + \$15 + \$8.50 = \$58.50
  - Total Fees Converted to ZAR: $35 * 17.64 = R617.40 + R264.60 + R150 = R1,032.00
- The settlement amount is the amount that finally ends up in the destination account,
calculated as follows:
  - Settlement Amount: Source converted to ZAR - Total Foreign Fees = R17,640 - R414.60 = R17,225.40
  - USD value of the settlement amount is R17,225.40 / 17.64 = $976.49
- The total transaction amount is the sum of the source amount and the source transaction fee:
  - Total Transaction Amount: Source + Source Transaction Fee = \$1000 + \$35 = \$1035
  - Total Transaction Amount Converted to ZAR: $1035 * 17.64 = R18,257.40
Putting that in table form:

| Description                               | Amount (USD) | Amount (ZAR) |
|:------------------------------------------|-------------:|-------------:|
| Source amount                             |     $1000.00 |   R17,640.00 |
| Source transaction fee                    |       $35.00 |      R617.40 |
| Exchange rate fee = 1.5% * 17.64 * Source |       $15.00 |      R264.60 |
| Foreign transaction service fee           |        $8.50 |      R150.00 |
| Total foreign fees                        |       $23.50 |      R414.60 |
| Total fees                                |       $58.50 |    R1,032.00 |
| Settlement amount                         |      $976.49 |   R17,225.40 |
| Total transaction amount                  |     $1035.00 |   R18,257.40 |

#### CurrencyTransaction Class
With TDD, you start with the end in mind. I wrote a test in the new class called CurrencyTransactionTest.

Here's the process I followed:
1. Create Bank object instantiated @BeforeAll tests.
2. Add the USD to ZAR and ZAR to USD rates to the Bank object.
3. Create a test that instantiates a CurrencyTransaction object with the source amount and the
target currency.
4. Set the Source Fee, Currency Rate Fee Percentage, and Foreign Transaction Fee.
5. Call the settle method on the CurrencyTransaction object to have it perform all the calculations.
6. Assert all the values in the table above.
7. Run the test and watch it fail.
8. Implement the CurrencyTransaction class to make the tests pass one at a time.
There are 13 asserts in the test, which is too many and it definitely needs to be reduced, but 
it allowed me to figure out how to implement the entire CurrencyTransaction class and then figure
out how to refactor it, and provide working code.

### Notes
1. You may be wondering why I added a settle() method. Well the example above is pretty simple.
In the real world, the money is sent on one day, and settled on a later day and the exchange
rate may have changed. The settle() method allows you to calculate the settlement amount
based on the exchange rate on the day the money is settled. 
2. Another thing you may be wondering is why I didn't split the CurrencyTransaction class into
two classes, one for the transaction and one to process it. Remember: This is Test Driven Development. 
I don't know what the final implementation will look like, so I'm not going to split the class
until I have a good reason to do so. I'm going to have to serialize this object later, and I really
wasn't sure what properties I need for that so the first step is just to get the class working.
3. I decided to handle the subtraction in the settle() method for now. I wanted to translate this
code to C# before I did the subtraction, on the expression because C# allows operator overloading and 
I'm starting to think that the Expression class is trying to do too much.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] Review the design decisions made
- [X] Money rounding?
- [x] Return `Money` from \$5 + \$5
- [x] 100% code coverage
- [x] `hashCode()`
- [x] Equal null
- [x] Equal object
- [x] `Sum.plus`
- [x] `Expression.times`
- [x] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 + \$5 = $10
- [x] Reduce `Money` with conversion
- [x] `Reduce (Bank, String)`
- [x] `Bank.Reduce(Money)`
- [x] \$5 * 2 = $10
- [x] Make "amount" private
- [x] Dollar side-effects?
- [x] `equals()`
- [x] 5 CHF * 2 = 10 CHF
- [x] Dollar/Franc duplication
- [x] Common Equals
- [x] Common Times
- [x] Compare Francs with Dollars
- [X] Currency?
- [x] Delete `testFrancMultiplication()`

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is
released and I only update it for Long Term Support (LTS) versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2

# Arithmetic Fixes - ch17-04-arithmetic
Now that I have the ability to perform Currency Transactions, I need to fix the arithmetic problem.
I want to be able to add, subtract, multiply, and divide Money objects. I also want to be able to
add, subtract, multiply, and divide Money objects with a decimal or an integer value.

## Thoughts on how to solve the problem
- Refactor the Expression class (Rethink this completely):
  - Rename it to Reduceable
  - Methods:
    - Add
    - Subtract
    - Multiply
    - Divide
    - Reduce
  - Supports generics that all implement IReduceable
  - All methods return a <T implements IReduceable> T and accept <T implements IReduceable> T parameters
  - The idea with "Sum" is that it is an expression that can be chained. Two things about this implementation are weird:
    1. Reduce actually does the addition. Reduce is therefore doing 2 things:
       - Converting to the target currency, and 
       - Adding items together.
    2. Reduce needs to be split into two functions. - Convert, and apply.
    3. Sum contains two operations - Plus & Times. Each operation should be its own class.
  - Another thing about Sum is that Generics did not exist when this code was written. This is a strong case for generics.
  - Also, in C# we have operator overloading, so we should be able to just say:
  ```c#
    Money a = new Money(20m, "USD");
    Money b = new Money(10m, "CHF")
    Money add = a + b;
    Assert.That(new Money(25m,"USD), Is.EqualTo(add));
  ```
## Subtraction
The second problem was a little more difficult and I needed to have real examples to work with before
I figured out how to solve it, so I deferred solving the subtraction issue until I had real examples 
of CurrencyTransactions to work with. I ended up solving the subtraction issue in the 
CurrencyTransaction class, which is not where it should be solved. This code will be refactored later.

## What I ended up with
The thought process I went through to get here was useful, but I hit the wall pretty quickly. I went back to the 
Gang of Four design patterns and concluded that I had one of two options in terms of patterns:
1. Visitor pattern. Treat each operation as a visit. I didn't like how this would lose the encapsulation.
2. Command pattern. Treat each operation as a command, which is really what it is.

I opted for the command pattern.

To do so, I needed a clear contract for the Money objecct, so I created several interfaces:
- ```ICurrencyHolder``` encapsulates the behavior for a currency container. It also 
requires a ```Bank``` property that contains the Bank object that the Money object was 
created against. It is needed to avoid having to pass a Bank with each arithmetic 
operation.
- ```ICurrencyConverter``` contains the ```Convert``` method that takes a currency holder and a target currency.
- ```IOperation``` contains an ```Apply``` method to handle the actual operation.
- ```IExpression``` contains the ```Add```, ```Subtract```, ```Divide``` and ```Multiply``` methods.

Additionally, I added the following class:
- ```Operation``` encapsulates the behavior for an operation an ostensibly replaces Sum

Finally, I did away with the following classes:
- ```Sum``` - not needed because Operation replaces it and the operation implementation 
can be done with lambda expressions
- ```SumTest``` - not needed because the Sum class was deprecated
- ```Expression``` - not needed because there is no base functinality to override. I may add it back.

I replaced the ```Reduce()``` method with ```Convert()``` because I wanted to distinguish
converting currency from the expression evaluation. There are two versions of ```Convert```:
1. In the ```ICurrencyHolder``` where it only takes the target currency as a parameter.
2. In the ```ICurrencyConverter``` where it takes an ```ICurrencyHolder``` and a string for the target currency as parameters.

For clarity:
- ```Money``` now implements ```ICurrencyHolder``` and ```IExpression```; and
- ```Bank``` now implements ```ICurrencyConverter```.

When I implemented ```IExpression``` in ```Money```, I also added operator overloading for 
```+```, ```-```, ```*```, ```/```, ```==```, and ```!=```.

This is where things got interesting. To add two ```Money``` objects together, I had to be 
convert them both to the same currency. I made the decision that with operator overloading,
I would use the currency of the first operator in the expression. So:
```c#
  Money usd = Money.For(20m, "USD");
  Money zar = Money.For(17m, "ZAR");
  Money result = usd + zar;
```
results in a value of ```21 USD``` in ```result``` assuming the exchange rate is 17:1. 

This was an important thing to decide, because it initially broke the unit tests. This test:
```c#
    public void TestMixedAddition(string fromCurrency, 
                                  decimal fromAmount, 
                                  string toCurrency, 
                                  decimal toAmount, 
                                  decimal expected)
    {
        testReduceHarness(fromCurrency, 
                          fromAmount, 
                          toCurrency, 
                          toAmount, 
                          expected, 
                          (from, to) => from + to);
    }
```
had to be changed so that the order of the operands in the lambda were swapped:
```c#
    public void TestMixedAddition(string fromCurrency, 
                                  decimal fromAmount, 
                                  string toCurrency, 
                                  decimal toAmount, 
                                  decimal expected)
    {
        testReduceHarness(fromCurrency, 
                          fromAmount, 
                          toCurrency, 
                          toAmount, 
                          expected, 
                          (from, to) => to + from);
    }
```
so that the result is returned in the ```to``` currency, not the ```from``` currency.

It also had impacts in the ```CurrencyTransaction``` class. As an example:
```c#
TotalTransactionFees = SourceFee.Convert(TargetCurrency) + TotalTargetFees;
```
Note that the ```TotalTransactionFees``` need to be in the target currency, but the ```SourceFee``` is in the source currency, so I call the ```Convert``` method before 
the addition to make sure I get it back in the target currency.



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


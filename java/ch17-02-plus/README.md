# Test-Driven Development - Java - Chapter 17 Part 2

In Chapter 17 of the book ["Test-Driven Development By Example" by Kent Beck](https://a.co/d/1sr05eT), Kent writes a retrospective on the
development of the example application in the previous chapters. He tasks the reader with going back and doing a few 
things:
- Make the tests more comprehensive - Done in ch17-01
- Fix the "Return `Money` from \$5 + \$5" TODO item
- Review the design decisions made

This solution is the second part of a four-part series that addresses these tasks. This part focuses on fixing Sum.

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 17 - Part 2 - Plus
One of the things Kent points out in the retrospective is that the `Plus` method in the `Sum` class should return a
`Money` object when the two `Expression` objects being added are both `Money` objects and the same currency. I decided
to solve this problem just for the sake of the exercise, even though I thought there are better ways to solve this 
problem. I decided to finish the TODO list that Kent adds in the retrospective, and then make fixing the design a part 
of the "Review the design decisions made" TODO item later.

The solution I came up with is to change the `Expression` interface into an abstract class and move the `Sum.Plus()` method
into the `Expression` base class so that all `Expression`s will have that `Plus` behavior. I made the `Plus` method
`virtual` so that it can be overridden in the derived classes. In the `Money` class, I override the `Plus` method to
check if the `addend` is also a `Money` object and if the currencies are the same. If they are, I return a new `Money`
object with the sum of the amounts. If not, I call the base class implementation which returns a polymorhic
`Expression` object.

This solution works, but I don't think it's the best solution. It adds complexity to the `Expression` class and
requires knowledge of the `Money` class. It also doesn't scale well if we add more types of `Expression`s in the future.

I used the `TestPlusSameCurrencyReturnsMoney` test to drive the development of this feature and removed the `Ignore`
attribute from the test. I also had to change the type check in the `TestSimpleAddition` test to check for `Money` instead 
of `Sum`. 

### Key learning
This exercise reinforced the idea that you can get a solution to work and produce value and outcomes. Sometimes it's more
important to get something working than to get it perfect. You can always go back and refactor later.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [x] Return `Money` from \$5 + \$5 
- [ ] Money rounding?
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
I try and keep this code up to date with the latest versions. I generally wait until a new version of the JDK or Maven is 
released and I only update it for major versions. JDK 25 is the latest version as of this writing, and the POM is set to
use JDK 25 and JUnit 5.13.4.

This repository was last updated in September 2025.
- Java JDK version 25
- JUnit version 5.13.4
- JetBrains IntelliJ IDEA Ultimate version 2025.2.2
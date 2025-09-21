# Test-Driven Development - C# - Chapter 17 Part 1

In Chapter 17 of the book ["Test-Driven Development By Example" by Kent Beck](https://a.co/d/1sr05eT), Kent writes a retrospective on the
development of the example application in the previous chapters. He tasks the reader with going back and doing a few 
things:
- Make the tests more comprehensive
- Fix the "Return `Money` from \$5 + \$5" TODO item
- Review the design decisions made

This is solution is the first part of a four-part series that addresses these tasks. This part focuses on the the tests.

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 17 - Part 1 - Tests
In this solution, I went back to the test code and rebuilt it to be more comprehensive and produce 100% code coverage. It
forced me to become intimately familiar with the testing frameworks for both C# and Java. I used NUnit for C# and JUnit 
for Java.

Both NUnit and JUnit have `Fixtures` and `TestCase`s that I was not using before this change. The nice thing about 
implementing `Fixtures` is that I could eliminate a lot of duplicate code. The `TestCase`s allowed me to run the same
test with different parameters. This was especially useful for the `CurrencyPair` tests.

JetBrains tools (Rider and IntelliJ IDEA) has a built-in pane for test runs. Structuring the tests properly and using test
cases makes it much easier to see what is going on in the tests in that pane.

The same is true for code coverage. JetBrains tools have a built-in code coverage tool that work very well to see exactly
what code is covered and what is not. I was able to get 100% code coverage in both the C# and Java versions as a result.

I also cleaned up three niggly items that were really bugging me:
- `hashCode()`/`GetHashCode()` was not properly implemented. I fixed this in both the C# and Java versions for both 
`CurrencyPair` and `Money`.
- `equals(null)`/`Equals(null)` was not properly implemented. I fixed this in both the C# and Java versions for both 
`CurrencyPair` and `Money`.
- `equals(object)`/`Equals(object)` was not properly implemented. I fixed this in both the C# and Java versions for both 
`CurrencyPair` and `Money`.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [x] 100% code coverage
- [ ] Return `Money` from \$5 + \$5
- [ ] Money rounding?
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
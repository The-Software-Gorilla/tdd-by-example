# Test-Driven Development - C# - Chapter 17 Part 2

In Chapter 17 of the book ["Test-Driven Development By Example" by Kent Beck](https://a.co/d/1sr05eT), Kent writes a retrospective on the
development of the example application in the previous chapters. He tasks the reader with going back and doing a few 
things:
- Make the tests more comprehensive
- Fix the "Return `Money` from \$5 + \$5" TODO item
- Review the design decisions made

This is solution is the second part of a four-part series that addresses these tasks. This part focuses on fixing Sum.

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 17 - Part 2 - Plus


### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] Return `Money` from \$5 + \$5
- [ ] Money rounding?
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
- [x] 100% code coverage
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
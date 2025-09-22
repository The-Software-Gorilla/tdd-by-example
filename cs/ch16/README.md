# Test-Driven Development - C# - Chapter 16

This solution contains code for Chapter 16 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 16 - Abstraction Finally
This chapter finalizes the work on mixed currencies. The changes are focused the arithmetic operations across currecies.

By the end of the chapter, we have a working solution with a few pieces that need to be cleaned up.

### Key point from the chapter
Kent makes a key point about TDD in this chapter. He says:
> You will likely end up with about the same number of lines of test code as model code when implementing TDD.

There are a couple of things about this to bear in mind:
1. The tests are not just for verifying the code works. They are also a form of documentation. They show how the code is 
intended to be used and what the expected behavior is.
2. These tests are part of a code base that requires continued maintenance. As additional features are added, the tests 
will help ensure that existing functionality is not broken, and they will need to be adjusted to cater for new 
functionality.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] Return `Money` from \$5 + \$5
- [x] `Sum.plus`
- [x] `Expression.times`
- [ ] Money rounding?
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
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
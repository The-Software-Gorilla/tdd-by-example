# Test-Driven Development - C# - Chapter 15

This solution contains code for Chapter 15 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 15 - Mixed Currencies
This code finally adds support for mixed currencies. The changes are relatively simple because of the way we have built
up to them and it is a good example of how TDD can help us build complex functionality in small, manageable steps. It 
almost feels like this is just cleanup work. 

### Key point from the chapter
The old riddle "How do you eat an elephant?" is answered with "One bite at a time." This chapter is a great example of 
that. We have a complex problem to solve, but we break it down into small pieces and solve each piece in turn. That's the 
essence of TDD and when you pair it with Agile principles, you can build complex systems in a manageable way.

I use the idea of a "TODO list" as the backlog of work to be done in an Agile project. Each item on the list is a small 
piece of functionality that we can implement in a single step. As we complete each item, we move it to the done column.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [x] \$5 + 10 CHF = $10 if rate is 2:1
- [ ] Return `Money` from \$5 + \$5
- [ ] `Sum.plus`
- [ ] `Expression.times`
- [ ] Money rounding?
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
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
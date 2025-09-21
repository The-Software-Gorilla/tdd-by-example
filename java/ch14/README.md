# Test-Driven Development - C# - Chapter 14

This solution contains a code for Chapter 14 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 14 - Change
This chapter introduces the `CurrencyPair` class to represent currency pairs for exchange rates. The `Bank` class
is modified to use `CurrencyPair` as the key in its exchange rate dictionary. The `AddRate` method in the `Bank` class
now takes two currency strings and an integer rate. The `GetRate` method in the `Bank` class is updated to use the
`CurrencyPair` class to look up rates.

### Key point from the chapter
The most important point to learn from in this chapter is when the tests fail looking for a USD to USD exchange rate. The
solution is to return 1 when the source and target currencies are the same. We also added a test for this case. The value
is that we get a failure here that we weren't expecting, which is a sign that something is wrong in our code, but the 
tests catch it. This is a good example of how TDD can help us catch unexpected issues in our code.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 + \$5 = $10
- [ ] Return `Money` from \$5 + \$5
- [x] Reduce `Money` with conversion
- [x] `Reduce (Bank, String)`
- [ ] Money rounding?
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
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
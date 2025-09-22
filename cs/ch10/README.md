# Test-Driven Development - C# - Chapter 10

This solution contains code for Chapter 10 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 10 - Interesting Times
The big takeaway from this chapter is how Kent approaches the process of refactoring. He gets each of the subclasses to
have exactly the same code and then he introduces a common method in the base class. This is where TDD really shines. The
refactor of this example is relatively simple but when you have a large code base with many dependencies, TDD can really
help as long as the tests are well written and very strongly typed.

By the end of this chapter, the `Dollar` and `Franc` classes no longer have any code in them. They simply call the base 
class. To all intents and purposes, they could be deleted but they still need to exist because the factory methods in the
`Money` class refer to them.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [x] Make "amount" private
- [x] Dollar side-effects?
- [ ] Money rounding?
- [x] `equals()`
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
- [x] 5 CHF * 2 = 10 CHF
- [ ] Dollar/Franc duplication
- [x] Common Equals
- [x] Common Times
- [x] Compare Francs with Dollars
- [X] Currency?
- [ ] Delete `testFrancMultiplication()`

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS) versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
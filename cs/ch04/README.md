# Test-Driven Development - C# - Chapter 04

This solution contains code for Chapter 4 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 4 - Privacy
I think this chapter points out a key aspect of TDD that is often overlooked. Thge tests you write should read like you
are describing the behavior of the system. In other words, the tests themselves are a form of documentation that 
describes the intent of the code. The refactor of the `TestMultiplication` test leads to a more readable test
that makes the intent much clearer. 

Put another way, the tests are as important to write clearly as the production code is.

The main change to the Dollar class in this chapter is to make the `Amount` member private. 

Something the book does not address about this change is that the `TestConstruction()` test no longer compiles as it is
trying to access a private member. The solution is to either delete the equality test in the `TestConstruction()` test 
or do an `Is.EqualTo(new Dollar(5))` comparison. I chose to do the latter because I always want my constructor to be
tested for its values too. Again, I would probably have implemented this as a `record` in C# as it would have been
immutable by default and implemented value equality, but I have stuck with the book's implementation.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [x] Make "amount" private
- [x] Dollar side-effects?
- [ ] Money rounding?
- [x] equals()
- [ ] hashCode()
- [ ] Equal null
- [ ] Equal object


## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS)versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
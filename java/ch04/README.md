# Test-Driven Development - Java - Chapter 04

This solution contains code for Chapter 4 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 4 - Privacy
I think this chapter points out a key aspect of TDD that is often overlooked. Thge tests you write should read like you
are describing the behavior of the system. In other words, the tests themselves are a form of documentation that 
describes the intent of the code. The refactor of the `testMultiplication` test leads to a more readable test
that makes the intent much clearer. 

Put another way, the tests are as important to write clearly as the production code is.

The main change to the Dollar class in this chapter is to make the `amount` member private. 

Something the book does not address about this change is that the `testConstruction()` test no longer compiles as it is
trying to access a private member. The solution is to either delete the `assertEquals(5, five.getAmount())` equality test 
in the `testConstruction()` test or do an `assertEquals(new Dollar(5))` comparison. I chose to do the former because 
I always want my constructor to be tested for its values. 

This is not the same as what I did in the C# version of this code. In C#, I made the `amount` member private and had
the equality test in `testConstruction()` assert against a new `Dollar(5)` object.

Again, I would probably have implemented this as a `record` in Java as it would have been immutable by default and 
implemented value equality, but I have stuck with the book's implementation.

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
I try and keep this code up to date with the latest versions. I generally wait until a new version of the JDK or Maven is 
released and I only update it for major versions. JDK 25 is the latest version as of this writing, and the POM is set to
use JDK 25 and JUnit 5.13.4.

This repository was last updated in September 2025.
- Java JDK version 25
- JUnit version 5.13.4
- JetBrains IntelliJ IDEA Ultimate version 2025.2.2
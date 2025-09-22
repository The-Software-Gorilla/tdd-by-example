# Test-Driven Development - Java - Chapter 11

This solution contains code for Chapter 11 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 11 - The Root of All Evil
This chapter gets rid of the `Dollar` and `Franc` subclasses and fixes the factory methods in the `Money` class to return
instances of `Money` directly. The tests still pass and the code is much simpler.

I went a little bit further than the book by adding a `Rand` factory method to the `Money` class as a shoutout to my 
South African roots. The `Rand` method is not mentioned in the book but it made it fun to have it here. It also proved
that the code was working correctly as I was able to create `Rand` instances without any changes to the existing code.

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
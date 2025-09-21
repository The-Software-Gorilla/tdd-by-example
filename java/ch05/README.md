# Test-Driven Development - Java - Chapter 05

This solution contains code for Chapter 5 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 5 - Franc-ly Speaking
I nearly lost my mind with this chapter. It's literally a copy and paste job of the Dollar class to create a Franc class 
and it's tests. That said, it does a good job of illustrating the point that code duplication is a really good way to 
highlight where abstractions are needed and it also lends itself well to using GenAI to help with the refactoring.

At the time the book was written, GenAI was not an option, so it's good to work through this abstraction manually.

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
- [x] 5 CHF * 2 = 10 CHF
- [ ] Dollar/Franc duplication
- [ ] Common Equals
- [ ] Common Times

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of the JDK or Maven is 
released and I only update it for major versions. JDK 25 is the latest version as of this writing, and the POM is set to
use JDK 25 and JUnit 5.13.4.

This repository was last updated in September 2025.
- Java JDK version 25
- JUnit version 5.13.4
- JetBrains IntelliJ IDEA Ultimate version 2025.2.2
# Test-Driven Development - Java - Chapter 03

This solution contains a code for Chapter 3 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 3 - Equality for All
This chapter does a good job of explaining the value object pattern and how to implement equality for value objects.
Martin Fowler has a good article on the [Value Object pattern](https://martinfowler.com/bliki/ValueObject.html) that is 
worth reading that describes the pattern in more detail. He also has a book called [Patterns of Enterprise Application
Architecture](https://a.co/d/bDrbiQw) that describes the pattern in more detail. There's also a reasonably good [Wikipedia
article](https://en.wikipedia.org/wiki/Value_object) that gives examples in several languages, including C#, Java & Python.
If I were implementing this in the real world, I would probably use a `record` in Java as they are immutable by default 
and implement value equality.

### Triangulation
Kent introduces the concept of triangulation in this chapter. The idea is that you write three tests that cover the same 
functionality in different ways. This gives you confidence that your implementation is correct.

A lightbulb thought for me was that triangulation is best used when you're trying to figure out how to refactor code.
You write three tests that cover the same functionality in different ways, then you refactor the code until all three
tests pass. This gives you confidence that your refactoring is correct.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [ ] Make "amount" private
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
# Test-Driven Development - C# - Chapter 09

This solution contains a code for Chapter 9 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 9 - Times We're Livin' In
This chapter continues removing duplication with the following changes:
1. It introduces a `Currency` property for the `Money` class and its subclasses.
2. It removes all constructor code for the `Dollar` and `Franc` classes and invokes the base class constructor.
3. It changes the `Times` method in the `Dollar` and `Franc` classes to use the factory methods in the `Money` class.

### Observations about the changes
This is the first time in the examples code where I used new language features that were not available in Java in 2002.
The `Currency` property is a read-only property in C#. The equivalent in Java would be a getter method.

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
- [ ] Common Times
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
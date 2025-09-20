# Test-Driven Development - C# - Chapter 08

This solution contains a code for Chapter 8 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 8 - Makin' Objects
This chapter focuses on removing duplication with two key changes:
1. Generailizing the `Times` method to return `Money` instead of `Dollar` or `Franc`.
2. Introducing factory methods in the `Money` class for construction of the `Dollar` and `Franc` objects.

### Observations about the changes
The changes to the code in this chapter are pretty extensive. The biggest changes are to the tests, not so much the
application code.

#### Generalizing the `Times` method to return `Money`
- While the code change here is pretty small, the key thing to note about the difference between the Java and C# versions
is that in C# you have to use the `override` keyword when overriding the `Times` method in the `Dollar` and `Franc` 
classes. In Java, the `@Override` annotation is optional, but in C# it is required.
- Even though the return type is changed to `Money`, the actual object returned is still a `Dollar` or `Franc` object. 
This is important in the tests at this stage because the constructor tests for `Dollar` and `Franc` are still in place 
and still type check the returned object to make sure they are `Dollar` and `Franc` objects.

#### Introducing factory methods in the `Money` class
- One key change that resulted in a lot of code changes that is not called out in the book is that the constructors for
`Dollar` and `Franc` have to be made `internal` in C#. This guarantees that the constructors can only be called from
within the same assembly. I achieve the same thing in Java by making the constructors package-private which means I 
don't specify the protection level.

### `TestFrancMultiplication()` method
The book references the `TestFrancMultiplication()` method in the `MoneyTest` class. This method is not in the code for
this chapter because I always start from the premise that every class should have a corresponding test class. So the 
`FrancTest` class has the equivalent of this method in the `TestMultiplication()` method.

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
- [ ] Currency?
- [ ] Delete `testFrancMultiplication()`

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS) versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
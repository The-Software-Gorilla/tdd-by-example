# Test-Driven Development - Java - Chapter 13

This solution contains code for Chapter 13 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 13 - Make It
This chapter introduces the 'Sum' class that implements the 'Expression' interface. The 'Plus' method in the 'Money' class
now returns a 'Sum' object. The 'Bank' class is introduced to handle currency conversions. The 'Reduce' method in the
'Bank' class takes an 'Expression' and a currency and reduces the expression to a single 'Money' object in the
given currency.

### Key point from the chapter
The `amount` field in the `Money` class needed to be exposed as a property so that it could be accessed from the `Sum`
class. For the Java version of the code, I did that a while back.

I have a rule that I never want to expose anything that is not absolutely necessary, so I my own TDD approach to this
would have been to have more explicit intention about the `amount` property, and refactor the factory methods in `Money`
for the specific implementations. I changed the code here so that the `amount` property is `public` but `set` is `private`.

### My thoughts on the chapter
This is one of the worst chapters in the book. No matter how many times I read it, I don't understand the premise that
Kent starts from. On page 62, he writes:
> First, Money.plus() needs to return a real `Expression` - a `Sum`, not just a `Money`.

He never expands on why and it is this line of reasoning that leads us down the road of creating the `Sum`class that is 
not necessary today. I suspect that the limitations of Java 1.4 had a lot to do with this decision. If I understand
his rationale, each arithmetic operation should return an `Expression` object so there would be one for each of the four
basic arithmetic operations. This lends itself to a Command pattern implementation, which is what I ended up doing
in [Chapter 17-04](../ch17-04-arithmetic/README.md), but it is way more convoluted.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [ ] \$5 + \$5 = $10- [ ] Money rounding?
- [ ] Return `Money` from \$5 + \$5
- [x] `Bank.Reduce(Money)`
- [ ] Reduce `Money` with conversion
- [ ] `Reduce (Bank, String)`
- [ ] `hashCode()`
- [ ] Equal null
- [ ] Equal object
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
I try and keep this code up to date with the latest versions. I generally wait until a new version of the JDK or Maven is 
released and I only update it for major versions. JDK 25 is the latest version as of this writing, and the POM is set to
use JDK 25 and JUnit 5.13.4.

This repository was last updated in September 2025.
- Java JDK version 25
- JUnit version 5.13.4
- JetBrains IntelliJ IDEA Ultimate version 2025.2.2
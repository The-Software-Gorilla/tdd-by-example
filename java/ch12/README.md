# Test-Driven Development - C# - Chapter 12

This solution contains a code for Chapter 12 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 12 - Addition, Finally
This chapter adds support for addition of currencies. The `Money` class now has a `Plus` method that returns an 
`Expression` object. It also adds the `Bank` class that is responsible for reducing an `Expression` to a single `Money` 
object in a given currency.

There are two distinctly different problems that are being solved in this chapter:
1. How to represent the addition of two `Money` objects.
2. How to convert currencies using exchange rates.

### Key point from the chapter
The most important point that I took away from this chapter is in the first paragraph on page 56:
> I will immediately go much slower - not in getting the tests working, but in writing the test itself. There are
> times and tests that call for careful thought.

The point Kent is making here is that TDD is not just about writing tests and getting them to pass. It's about thinking
about the long-term testing and design of the code and it's really important to do this with code that you know is going
to be sensitive to change later.

### My thoughts on the chapter
As I reread this chapter 22 years later, I felt it failed to address the two problems distinctly, and the resulting code 
starts creating a bigger problem that is ultimately not solved in the book. I suspect that this is because the book is 
trying to illustrate TDD and not design patterns, but the problem is that the design of the code is going to have a big 
impact on the ability to handle arithmetic operations on the `Money` class in the future, and it ends up being a much
bigger refactoring issue at the end of Chapter 17.

This was also the chapter where I really struggled with not using operator overloading in C#. It was this chapter that
decided me that I'd finish the book examples as a reference implementation, but they are not the base material for my
TDD classes. This is where my own examples start diverging from the book examples.

Another issue I have with this chapter is that the new `Bank` class is misnamed. The purpose of the class is to hold the 
exchange rates that is effective for the currency conversion. The problem is that the `Bank` class is not really a bank. 
It's just a reference to an exchange rate table effective at the time of conversion. Bearing in mind that we want to be 
able to convert currencies in both directions, and we want to be able to handle multiple currencies, the `Bank` class 
actually needs to hold a table of exchange rates at a specific point in time. I therefore think a better name for the 
class would be `ExchangeRateTable`.

One takeaway from this chapter is how decisions made here can turn into significant technical debt later.
This is one of the weaknesses of TDD - it doesn't always lead to the best design. It leads to a design that works and
delivers immediate value, but may not be very extensible later. This was one of those times where I would have taken a 
step back and thought about the appropriate pattern to use before I moved forward. 

I ended up solving this problem as a self-learning exercise after I finished the book examples. I don't remember doing 
that 22 years ago. The resulting code was used a Command pattern implementation. In the [README for the code in 
Chapter 17-04](../ch17-04-arithmetic/README.md) that addresses this design issue, I walk through the decisions that I 
made to fix the problem. The implementation I ended up with is definitely not perfect, but it is a lot more extensible 
than the implementation in the book. In my own TDD classes, I use a similar implementation, but I start it a lot earlier.

Another example of the problem with this book is the choice that was made back in Chapter 1 to make the `amount` field an 
integer. Currency amounts are almost always represented as decimal numbers. The further we get into the book, the more 
code we have that exacerbates this problem. Again, I would have taken a step back and thought about the appropriate
way to represent currency amounts before I moved forward. I ended up addressing this problem in 
[Chapter 17-03](../ch17-03-decimal/README.md).

That said, I need to give Kent Beck credit for writing a book that has stood the test of time. I haven't been able to go
back and build this example using the Java version ("Merlin" or J2SE 1.4) that was available at the time. J2SE 1.4 was 
released in February 2002 and J2SE 1.3 ("Kestrel") was released in May 2000, so he could have been using either of them
when he wrote the book.

Oracle bought Sun Microsystems in 2009 and Java 1.4 support was stopped by Sun in 2008. While there are binaries of the 
J2SE 1.4 SDK available from Oracle, I'm on a Mac with an M4 chip and I don't think it's worth the effort to try to get
it to run.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [ ] \$5 + \$5 = $10
- [ ] Money rounding?
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
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS) versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
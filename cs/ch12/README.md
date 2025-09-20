# Test-Driven Development - C# - Chapter 12

This solution contains a code for Chapter 12 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 12 - Addition, Finally
This chapter adds support for addition of currencies. The `Money` class now has a `Plus` method that returns an 
`Expression` object. This was one of the chapters that I felt was a bit more contrived than it needed to be, and the
resulting code starts creating a problem that is ultimately not solved in the book. I ended up solving this problem
after the end of the Chapter 17 code was complete using a Command pattern implementation.

I think the one takeaway from this chapter is how a decision made here can turn into significant technical debt later.
To me, this is one of the weaknesses of TDD - it doesn't always lead to the best design. It leads to a design that works, 
but is not very extensible and I this was one of those times where I would have taken a step back and thought about the
appropriate pattern to use before I moved forward. In the [README for the code in Chapter 17-04 that 
fixes it](../ch17-04-arithmetic/README.md), I walk through the decisions that I made to fix the problem.

Another example of the problem with TDD is the choice that was made back in Chapter 1 to make the `amount` field an 
integer. Currency amounts are almost always represented as decimal numbers. The further we get into the book, the more 
code we have that exacerbates this problem. Again, I would have taken a step back and thought about the appropriate
way to represent currency amounts before I moved forward. I ended up addressing this problem in [Chapter 17-03](../ch17-03-decimal/README.md).

It was this chapter that decided me that I'd finish the book examples as a reference implementation, but they are not
the base material for my TDD classes. This is where my own examples start diverging from the book examples.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 + \$5 = $10
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
# Test-Driven Development - C# - Chapter 02

This solution contains a code for Chapter 2 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 2 - Degenerate Objects
Kent starts this chapter by simplifying the process to the essential elements of TDD:
1. Write a test. (RED)
2. Make it run. (GREEN)
3. Make it right. (REFACTOR)

He then says **"The goal is clean code that works."** 

The more I have used TDD in the real world, the more I have come to appreciate this statement. The goal is not 
to have perfect code that is completely abstracted and reusable. The goal is to have clean code that is error free, 
easily understood, maintained, and tested so that any changes can be made with confidence.

### Key points from the chapter
Kent introduces the concept of the Value Object pattern. In the Chapter 2 implementation, when the `times()` method is 
called, the value of `Amount` is changed, hence the title of the chapter - Degenerate Objects. He walks us through how
he fixes this by returning an immutable value object. The idea is that the `Dollar` object should never change its state. 
Instead, a new object should be returned with the new state.

There's a key issue with the implementation in Chapter 2, though. The `Amount` member is public and settable. As I
explained in the [README for Chapter 1](../ch01/README.md), in C# I would normally implement this as a read-only 
property (in Java, it would be a getter only) , but I have kept it as a public member to stay true to the book until 
we make it private in Chapter 4.

Kent also introduces the three strategies for getting to green:
- **Fake it.** Return a constant and gradually replace it with real code.
- **Use obvious implementation.** Write the simplest code that could possibly work.
- **Triangulation.** He talks more about thus in Chapter 3.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [ ] Make "amount" private
- [x] Dollar side-effects?
- [ ] Money rounding?

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS)versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
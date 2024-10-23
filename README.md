# Test Driven Development Example Code
Back in 2004, I was working for a company that had one of the most extensive test suites I have ever seen (to this day). The majority of 
the codebase was written in C/C++ and even though we were using Rational Clearcase for version control, we had a very strict build
process that regression-tested all code during the nightly-build. 

We started development on a new product that involved extensions to Eclipse and we were using Java. We took the opportunity to
introduce some of the concepts of TDD into our development process, but we were not big proponents of building the tests first and then coding to make them succeed. 
We were a Rational Unified Process (RUP) shop and XP was still in its infancy.

A few years later, I was working for a company that was embarking on its Agile journey and we embraced TDD as part of that process. Since then, I have been a strong 
advocate for TDD and I have seen the benefits it brings to software development.

In the last few months I have seen a lot of posts about TDD on social media that have made me reflect on my own experiences with it. I decided to go back to the 
TDD Bible ["Test Driven Development: By Example" by Kent Beck](https://a.co/d/5tOeg2z) and work through the examples again. It's been at least 17 years since I last did it, and I never kept the code from back then.

<iframe type="text/html" sandbox="allow-scripts allow-same-origin allow-popups" width="336" height="550" frameborder="0" allowfullscreen style="max-width:100%" src="https://read.amazon.com/kp/card?asin=B095SQ9WP4&preview=inline&linkCode=kpe&ref_=kip_embed_taf_preview_WZ4954A2RQDFRZBJXH73" ></iframe>

A quote from the book that really caught my attention was this:
> You will often be implementing TDD in code that doesn't have adequate tests (at least for the next decode or so).
> - Kent Beck, "Test Driven Development: By Example," Chapter 6, page 29, (c) 2003

The book was written in 2002. I have worked at several companies since then and I think it is true to say that many codebases
still do not have adequate tests. In fact, I'd venture a guess that we have not made much progress in that area in the last 20
years for existing codebases and, in all likelihood, have a lot more code that is not tested than code that is.

I decided to work through the examples in the book again and create a repository to share the code with others who may want to do the same.

## About this Repository

The code in this repository is the example code that Kent Beck used in his book "Test Driven Development: By Example". 
It demonstrates the principles of Test Driven Development (TDD) chapter by chapter according to the examples in the book.
I have created separate examples for each chapter and I have also provided code in Java and C#. I may add a Python version later.

### Structure of the Repository
- There is a separate folder for C# (css) and Java (java) examples.
- I separated the examples into the money and xunit folders. 
- The money folder contains example code for chapters 1 to 17.
- The xunit folder contains example code for chapters 18 to 24.
- Each chapter has its own folder with the corresponding code.
- The code is organized to follow the TDD cycle.
- I initially thought about creating a separate branch for each chapter, but I decided against it because:
  - Branches cannot be constrained to a single folder, so I couldn't keep .NET code in one branch and Java in another so the code is easy to navigate.
  - It would be difficult to see the progression of the code through the chapters.
  - It would be difficult to see the differences between the Java and C# implementations.
- As the book is based on Java and JUnit, I had to take some liberties with the C# code because of the subtle differences between C#, Java, NUnit and JUnit.
- Bear in mind the book was written in 2002, so the Java and JUnit versions it was based on did not have support for lambdas, anonymous functions, etc. We're talking Java 1.4-ish. That said, the concepts are still as relevant today as they were 20-some years ago.

## Key Concepts from the Book

### Most Important Rules
The two most basic rules of Test Driven Development are:
- Write a failing automated test before writing any code.
- Remove duplication.

### The TDD Cycle
The TDD cycle consists of five main steps:
1. Quickly add a test.
2. Run all tests and see the new test fail. (It should almost always fail.)
3. Make a little change.
4. Run all tests and see them all pass.
5. Refactor the code to remove duplication.
> Remember, TDD is not about taking teeny-tiny steps, it's about *being able* to take teeny-tiny steps.
> - Kent Beck, "Test Driven Development: By Example"

### Three strategies for getting to green
1. **Fake it**: Return a constant and gradually replace it with variables until you have real code.
2. **Use Obvious Implementation**: Type in the real implementation.
3. **Triangulation**: Use two or more tests to find the right implementation.

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
TDD Bible - ["Test Driven Development: By Example" by Kent Beck](https://a.co/d/5tOeg2z) - and work through the examples again. It's been at least 17 years since I last did it, and I never kept the code from back then.

A quote from the book that really caught my attention was this:
> You will often be implementing TDD in code that doesn't have adequate tests (at least for the next decade or so).
> - Kent Beck, "Test Driven Development: By Example," Chapter 6, page 29, (c) 2003

The book was written in 2002. I have worked at several companies since then and I think it is true to say that many codebases
still do not have adequate tests. In fact, I'd venture a guess that we have not made much progress in that area in the last 20
years for existing codebases and, in all likelihood, have a lot more code that is not tested than code that is.

I decided to work through the examples in the book again and create a repository to share the code with others who may want to do the same.

## About this Repository

The code in this repository is the example code that Kent Beck used in his book "Test Driven Development: By Example". 
It demonstrates the principles of Test Driven Development (TDD) chapter by chapter according to the examples in the book.
I have created separate examples for each chapter and I have also provided code in Java and C#. I may add a Python and/or React version later.

### Structure of the Repository
- The money example spans 17 chapters and each language has complete example code for each complete chapter.
- The C# example is in the 'cs' folder. Development work was done using VSCode and C# 12, .NET 8 and dotnet CLI.
- The Java example is in the 'java' folder. Development work was done using IntelliJ IDEA, Java 23, JUnit 5 and Maven 3.9.9.
- Each language folder has a README.md that provides details on how to setup your environment for that language.
- Each language has folders numbered 'ch00' through 'ch17' and a 'money' folder.
- The 'ch00' folder contains an empty project for its language that can be used as starting point if you want to follow along yourself.
- The 'ch17' and 'money' folders contain code that is not in the book. See notes in the [next section](https://github.com/The-Software-Gorilla/tdd-examples#ch17-and-money-folders).
- The 'xunit' folder contains example code for chapters 18 to 24 in Python.
- Each chapter has its own folder with the corresponding code and a README.md specific to that chapter.
- The code is organized to follow the TDD cycle.
- I initially thought about creating a separate branch for each chapter, but I decided against it because:
  - Branches cannot be constrained to a single folder, so I couldn't keep .NET code in one branch and Java in another so the code is easy to navigate.
  - It would be difficult to see the progression of the code through the chapters.
  - It would be difficult to see the differences between the Java and C# implementations.
- As the book is based on Java and JUnit, I had to take some liberties with the C# code because of the subtle differences between C#, Java, NUnit and JUnit.
- Bear in mind the book was written in 2002, so the Java and JUnit versions it was based on did not have support for lambdas, anonymous functions, etc. We're talking Java 1.4-ish. That said, the concepts are still as relevant today as they were 20-some years ago.

### 'ch17' and 'money' folders
Chapter 17 of the book is a retrospective on the Money example. In it, Kent suggests a refactor of the Expression interface into a class. I decided to give that a try to see if I can resolve the "plus" duplication. He also suggested a refactor of the unit tests to leverage fixtures. I took that on in the Chapter 17 code.

The 'money' folder contains some additional changes I wanted to experiment with that do not relate to the book at all. I decided to add functionality to the application to fetch realtime currency values from an API at [FreecurrencyAPI](https://freecurrencyapi.com/). I wanted to be able to use the example to retrieve the exchange rates from the API in realtime and do the real conversions. This meant quite a bit of refactoring of currency values to support decimal-based conversions. I also used this section to create a command line utility to convert, add, and multiply currency, and I built some REST service endpoints to enable a web/mobile front-end. I may add the front-end later.

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

### Let the computer answer the reasoning questions
Chapter 10, page 46, Kent talks about one of the major benefits of a base of clean code with passing tests:
> Rather than apply minutes of suspect reasoning, we can just ask the computer by making the change and running the tests.

Instead of spending 5 to 10 minutes reasoning out a problem, TDD gives you the ability to ask the computer to solve the problem in 15 seconds. 
> Without the tests you have no choice, you have to reason. With the tests you can decide whether an experiment would answer the question faster.

### Code without a test
Chapter 10, page 47 has a paragraph that starts: "Whoa! Code without a test? Can you do that?" Kent then lays out three reasons 
why it is OK under certain circumstances to write code without a test:
1. We're about to see the results of the test on the screen.
2. Because the toString() method is being used only for debug purposes, the risk of failure is low.
3. We're in the process of running tests and we have a red bar (test failure). We'd prefer not to write a test when we have a red bar.

Kent then goes on to point out later on the page that the conservative course is to back out a change to get back to green, but in this case there would be no way to move forward without understanding what is making us red.

### Writing a test while red
Also in Chapter 10, page 47, Kent discusses the idea of writing a test while the tests are red. He goes ahead with it only when we're about to change the real model code and you can never do that without a test.

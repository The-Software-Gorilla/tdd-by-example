# "Test-Driven Development: By Example" Code Samples

In 2002, Kent Beck published his seminal book on Test-Driven Development (TDD), 
["Test Driven Development: By Example"](https://a.co/d/5tOeg2z). I call it "The TDD Bible." It is still one of the best 
books on TDD and I highly recommend it to anyone interested in learning more about TDD. 

The book did not come with a code sample repository, so I decided to create this repository to share the example code 
that Kent used in the book in both Java and C#. At some point, I may add Python and/or React versions as well.

The Java code is in the `java` folder and the C# code is in the `cs` folder. I have provided a README.md in each of those
folders that provides details on how to setup your environment for that language.

Underneath each language folder are folders numbered `ch00` through `ch17`. Each folder has a README.md that provides 
information and editorial comments about that chapter. The `ch00` folder contains an empty project for its language that 
can be used as starting point if you want to follow along yourself, *which I highly recommend you do*.

## My Journey with TDD
Back in 2004, I was working for a company ([Progress Software Corporation](https://www.progress.com/)) that had one of 
the most extensive test suites and some of the best engineering practices I have ever seen (to this day). 
The majority of the codebase was written in C/C++ and even though we were using Rational Clearcase for version control, 
we had a very strict build process that regression-tested all code during the nightly-build. 

We started development on a new product that involved extensions to Eclipse and we were using Java. We took the 
opportunity to introduce some of the concepts of TDD into our development process. We were looking for ways to make sure
we applied the same level of rigor to our Java solutions as we did to our core code base. At the time, though, we were a 
Rational Unified Process (RUP) shop and XP was still in its infancy.

A few years later, I was working for a company that was embarking on its Agile journey and we embraced TDD as part of 
that process. As a result, I have been a strong advocate for TDD and I have seen the benefits it brings to software 
development for the last 21 years (yeah, it really is that long!).

## Why this Repository?

There's a quote in the book that really sums up why I created this repository:
> You will often be implementing TDD in code that doesn't have adequate tests (at least for the next decade or so).
> - Kent Beck, "Test Driven Development: By Example," Chapter 6, page 29, (c) 2003

The book was written in 2002. 

Over the last few years, I have gone from working at companies that had a strong testing culture to companies that had
little to no testing culture. It seems to be true that the further you drift from technology-focused companies, the less
focus there is on testing. Kent Beck's own experience seems to confirm that as he joined Facebook 2011 and was surprised
to find that they had very little automated testing.

It's this lack of a testing culture that led to this repository for a couple of reasons:
1. I have taken on roles at several employers where I needed to establish a testing culture and I found
myself going back to the TDD Bible to refresh my memory on the principles of TDD so I could point my teams at a code base
that helps them ramp up.
2. In the last few months I have seen a lot of argumentative posts about TDD on social media that have made me reflect on 
my own experiences with it. TDD has saved me on more than one occasion, so the social media arguments made me think that
I should go back to the source and re-immerse myself in the principles of TDD.

That's what led to me going back to the TDD Bible again - ["Test Driven Development: By Example" by Kent Beck](https://a.co/d/5tOeg2z) - 
and working through the examples to create this repository. Although I have worked through the book several times,
I never kept the code and this time I decided to work through the examples in the book again and create a repository 
to share the code with others who may want to do the same.

## References
- [Test-Driven Development: By Example](https://a.co/d/5tOeg2z) - Book by Kent Beck
- [Is TDD Dead?](https://martinfowler.com/articles/is-tdd-dead/) - Conversation between Martin Fowler and Kent Beck
- [Test Desiderata](https://kentbeck.github.io/TestDesiderata/) - Kent Beck's website on testing
- [Software Design: Tidy First](https://tidyfirst.substack.com/) - Newsletter by Kent Beck


## About this Repository

The repository contains the code samples from the book in both Java and C# and each chapter contains a complete working
example that you can run and test yourself that illustrates the code for that chapter.

### Code Maturity
The book was written in 2002 and the code examples were based on Java 1.3 or 1.4, and I'm not sure what version of JUnit
was in place at the time. Things like generics, lambdas, records, and streams did not exist yet, and 
[fluent interfaces](https://martinfowler.com/bliki/FluentInterface.html) were not a thing back then. The code base
therefore intentionally limits the use of modern language features to stay true to the original code examples in the book.
That said, the concepts are still as relevant today as they were 20-some years ago.

### Development Environment
I use the [JetBrains suite of IDEs](https://www.jetbrains.com/all/) for all my professional development work, so I have 
used [JetBrains Rider](https://www.jetbrains.com/rider/) for the C# code and 
[IntelliJ IDEA Ultimate](https://www.jetbrains.com/idea/) for the Java code.

You can probably use Visual Studio or Visual Studio Code (VSCode) for the C# code and Eclipse or VSCode for the 
Java code, but I have not tried that myself because I only work on MacOS, Eclipse is slow and resource-greedy, and
VSCode is not a commercial-quality IDE.

### The TODO List
Kent Beck uses a TODO list in the book to keep track of what needs to be done next. I have included the TODO list
in each chapter's README.md file in the state it is in at the end of that chapter.

I have also added the TODO list to the code itself as comments in the `DollarTest` class through Chapter 5 and then in 
the `MoneyTest` class from Chapter 6. In chapter 12, Kent removes a whole bunch of TODO comments from the code, so I 
moved the ones that were done into a DONE list in the code.

**Before you complain about TODO's in the code:** The book was written in 2002. I simply wanted to stay true to the
original code examples in the book, warts and all. ***I would never write production code and check it into `main` with
TODO comments in it.***

Rider and IntelliJ IDEA both have TODO support that picks up comments in the code that start with TODO, so you can easily
find the list of TODOs in a tool pane in the IDE itself. I know VSCode and Eclipse have similar support. 

In the C# code, I have put the TODO comments in a `#region TODO List` block at the top of the `DollarTest` and `MoneyTest`
classes to make it easy to find and fold away. I know this works in Visual Studio and VSCode as well.

I did a similar thing in the Java code, where I put the TODO comments in a `//<editor-fold desc="TO DO List">`
block at the top of the `DollarTest` and `MoneyTest` so that you can easily find it and fold it away. I think this 
might be a feature specific to IntelliJ IDEA, so it may not work in other IDEs. I had to label the tag for the `editor-fold`
as `TO DO List` instead of `TODO List` because otherwise IntelliJ IDEA would pick it up as a TODO comment that will show
up in the TODO pane.

### Structure of the Repository
- The money example spans 17 chapters and each language has complete example code for each complete chapter.
- There is a [GitHub Actions CI/CD pipeline](./.github/workflows/tdd-by-example-tests.yml) that builds and tests 
both the C# and Java code on every push and pull request.
- The C# examples are in the `cs` folder. Development work was done using JetBrains Rider and .NET 8.0.414, NUnit 4.4.0 
and dotnet CLI.
- The Java example are in the `java` folder. Development work was done using IntelliJ IDEA, Java 24 and 25, JUnit 5.13.4 
and Maven 3.9.11. Java 25 was used on my local machine, but GitHub Actions only supports up to Java 24 at the time of 
writing, so the CI/CD pipeline uses Java 24. Note that I use Maven to build the Java code, not Gradle.
- Each language folder has a README.md that provides details on how to setup your environment for that language.
- Each language has folders numbered `ch00` through `ch16` and then 4 `ch17` folders numbered `ch17-01` through 
`ch17-04`.
- The `ch00` folder contains an empty project for its language that can be used as starting point if you want to follow 
along yourself.
- The `ch17` folders contain code that is not in the book. See notes in the [next section](https://github.com/The-Software-Gorilla/tdd-examples#ch17-and-money-folders).
- Each chapter has its own folder with the corresponding code and a README.md specific to that chapter.
- The code is organized to follow the TDD cycle.
- I initially thought about creating a tag for each chapter, but I decided against it because:
  - Tags cannot be constrained to a single folder, so I couldn't keep .NET and Java tags separated neatly.
  - It would be difficult to see the progression of the code through the chapters.
- As the book is based on Java and JUnit, I had to take some liberties with the C# code because of the subtle 
differences between C#, Java, NUnit and JUnit.

### 'ch17' folders
Chapter 17 of the book is a retrospective on the Money example. In it, Kent suggests a refactor of the Expression 
interface into a class. I decided to give that a try to see if I can resolve the "plus" duplication. He also suggested 
a refactor of the unit tests to leverage fixtures. I took that on in the Chapter 17 code. There are therefore 4 distinct 
ch17 folders:
1. **`ch17-01-tests`:** This folder contains refactored unit tests that use fixtures and test cases and extend the tests 
to get to 100% code coverage.
2. **`ch17-02-plus`:** This folder contains the code after the refactor to fix the duplicate plus issue. There are 
significant changes in both Java and C# that made it possible to create very clean code that was not possible in 2002.
3. **`ch17-03-decimal`:** This folder contains the code after I refactored the Money object to return decimal values 
instead of integers. It also contains contains the CurrencyTransaction class that was modeled on the foreign wire 
transfers that I do for family from time to time. 
4. **`ch17-04-arithmetic`:** This folder extends the currency arithmetic to support subtraction and division as well. It 
uses operator overloading in C# to make the code more natural and it also changed the addRate() method on the Bank class 
to enable reciprocal exchange rates.

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
> - Kent Beck, "Test Driven Development: By Example", Chapter 1, page 9

### Red, Green, Refactor
The TDD cycle is often summarized as "Red, Green, Refactor":
1. **Red**: Write a failing test.
2. **Green**: Write just enough code to make the test pass.
3. **Refactor**: Remove duplication and improve the design.

### Three strategies for getting to green
1. **Fake it**: Return a constant and gradually replace it with variables until you have real code.
2. **Use Obvious Implementation**: Type in the real implementation.
3. **Triangulation**: Use two or more tests to find the right implementation.

### Let the computer answer the reasoning questions
Chapter 10, page 46, Kent talks about one of the major benefits of a base of clean code with passing tests:
> Rather than apply minutes of suspect reasoning, we can just ask the computer by making the change and running the tests.

Instead of spending 5 to 10 minutes reasoning out a problem, TDD gives you the ability to ask the computer to solve the 
problem in 15 seconds. 

> Without the tests you have no choice, you have to reason. With the tests you can decide whether an experiment would 
> answer the question faster.

### Code without a test
Chapter 10, page 47 has a paragraph that starts: "Whoa! Code without a test? Can you do that?" Kent then lays out three 
reasons why it is OK under certain circumstances to write code without a test:
> 1. We're about to see the results of the test on the screen.
> 2. Because the toString() method is being used only for debug purposes, the risk of failure is low.
> 3. We're in the process of running tests and we have a red bar (test failure). We'd prefer not to write a test when we 
have a red bar.

Kent then goes on to point out later on the page that the conservative course is to back out a change to get back to 
green, but in this case there would be no way to move forward without understanding what is making us red.

### Writing a test while red
Also in Chapter 10, page 47, Kent discusses the idea of writing a test while the tests are red. He goes ahead with it
only when we're about to change the real model code and you can never do that without a test.

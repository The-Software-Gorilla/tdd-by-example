# Test-Driven Development - C# - Chapter 01

This solution contains code for Chapter 1 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests.

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 1 - The Multi-Currency Money Example
Kent starts this chapter with the process you should follow for TDD:
1. Quickly add a test.
2. Run all tests and see the new one fail.
3. Make a little change.
4. Run all tests and see them all pass.
5. Refactor to remove duplication.

He then starts out with a TODO list:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [ ] \$5 * 2 = $10

These TODO's are then added into the Money.Tests DollarTest class and we write the tests from there.
By the end of the chapter, that list has grown to this, and only the second item has been completed:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [ ] Make "amount" private
- [ ] Dollar side-effects?
- [ ] Money rounding?

### Key points from the chapter
The book was written in 2002 and the examples are in Java. Both Java and C# have matured since then. Things like
anonymous classes, generics and lambdas were not available in 2002, and operator overloading has never been available
in Java.

The disparity between C# & Java really showed up in this chapter. The `Amount` property in the `Dollar` class is a
member in Java, but in C# it could easily be implemented as a read-only property. In fact, my natural inclination was
to make it a read-only property, but the book needs it as a member until we set it private in Chapter 4. The reality
is that in C#, I would start with this as a read-only property, but I was trying to stay true to the book and deal with
the disparity between Java and C#.

Another one was operator overloading. In Java, you cannot overload operators, but in C# you can. I resisted the
temptation to overload the `+` and `*` operators in the `Dollar` class as I knew it would remove obstacles that the
book illustrates in later chapters.

Another feature that is available in both Java and C# is the ability to declare variables without specifying the type. 
In both languages you can use the `var` keyword and the compiler infers the type from the expression on the right-hand 
side of the assignment. The problem is that there is a lot of code in the book that uses very strong typing to help with 
refactoring later, so I deliberately avoided using `var` in this solution. This decision is debatable, but I felt it was 
more important to stay true to the book in the example code. There are definitely pros and cons to both approaches:
- Using `var` can make the code cleaner and easier to read, especially when the type is obvious from the context.
- Using explicit types can make the code more verbose, but it can also make it clearer what the type of a variable is,
which can be helpful with the refactoring to make sure your type changes are intentional.

For the sake of fidelity to the book, I have not used any of these features in this solution until we get to
Chapter 17. Like me, you'll probably look at the solution and think "I would have done that differently now," but I
found that when I diverged from the book, large sections became unnecessary. I made the decision to stick with the book
as the concepts of TDD that each chapter introduces are more important than the implementation.

### A note about TODO comments in the code
JetBrains Rider has a TODO window that shows all the TODO comments in the code and I use it extensively in real world 
code. I normally delete the TODO comments when I finish the task, but in this case I have left them in to show the
progression of the chapter and I have marked them as "- DONE" when they are completed.

From a development perspective, I have a rule that TODO comments should only ever appear in the **test** code, and never in
code that is checked into the main or dev branches after a pull request to merge them. For real code, the TODO comments
are normally a reminder to myself of what the Acceptance Criteria is from the user story I am working on. If I get to the
end of a user story and there are still TODO comments in the code, I have not finished the story.

Another thing to note is that when I am working on a user story, the TODO comments often become tasks in my issue 
tracking system (ADO or Jira) and I delete the TODO comments when I finish the task.

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS)versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
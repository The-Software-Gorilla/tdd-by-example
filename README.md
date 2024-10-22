# Test Driven Development Example Code
The code in this repository is the example code that Kent Beck used in his book "Test Driven Development: By Example". 
It demonstrates the principles of Test Driven Development (TDD) chapter by chapter according to the examples in the book.
I have created separate examples for each chapter and I have also provided code in Java and C#. I may add a Python version later.
> You will often be implementing TDD in code that doesn't have adequate tests (at least for the next decode or so).
> - Kent Beck, "Test Driven Development: By Example," Chapter 6, page 29, (c) 2003

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

# Test-Driven Development - Java - Introduction and Base Solution

This folder contains a base solution for the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in Java and uses the JUnit testing framework for the tests. 
I use this solution as the starting point for each chapter from the book.

**To be clear:** The README's in these folders do not contain the content of the chapters. They just contain a summary 
of what the code in the solution does and the changes for each chapter. Where it makes sense, I have provided notes on 
what is different from the book examples and why. 

**You should absolutely read the book and build each solution from scratch as you go.** Your solution may vary from
mine.

The following chapters all build from this base solution.

## Introduction Chapter
The Introduction introduces the core of TDD with two simple rules:
1. Write a failing automated test before you write any code.
2. Remove duplication.

This means that you are going to start your development in the test project, write a failing test, write the code to
make the test pass in the main project, and then remove duplication. This is the Red-Green-Refactor cycle that
is the essence of TDD.

This base solution contains no code. It is just the starting point for the examples in the book, but by the time you 
create a copy of this code for your own use, you should have read the Introduction chapter of the book. 

## Set up
To set up the repository, you need to have the Java JDK installed on your machine. You can download it from
[here](https://jdk.java.net/25/) or if you are on Mac you can use Homebrew:
```bash
brew update
brew install openjdk@25
```
Once you have the JDK installed, you can clone this repository and open it in your favorite IDE. I use JetBrains IntelliJ, 
but you can also use Eclipse or Visual Studio Code (VSCode). 

_**Note:** I have not tested this repository in Eclipse or VSCode_ as Eclipse is a bit of a resource pig and VSCode 
is not a professional quality IDE. 😲😜 (There. I said it.)

## Build and Run
This repository uses Maven as the build tool. To install Maven on a Mac, you can use Homebrew:
```bash
brew install maven
```
You can verify that Maven is installed by running:
```bash
mvn -v
```
You can build and run the tests using the following command:
```bash
mvn clean test
```

You can also run the tests from within your IDE. In IntelliJ, you can right-click on the `src/test/java` folder
and select "Run 'All Tests'".

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of the JDK or Maven is 
released and I only update it for major versions. JDK 25 is the latest version as of this writing, and the POM is set to
use JDK 25 and JUnit 5.13.4.

This repository was last updated in September 2025.
- Java JDK version 25
- JUnit version 5.13.4
- JetBrains IntelliJ IDEA Ultimate version 2025.2.2
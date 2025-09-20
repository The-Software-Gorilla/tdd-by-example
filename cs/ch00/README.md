# Test-Driven Development - C# - Introduction and Base Solution

This folder contains a base solution for the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 
I use this solution as the starting point for each chapter from the book.

**To be clear:** The README's in these folders do not contain the content of the chapters. They just contain a summary 
of what the code in the solution does and the changes for each chapter. Where it makes sense, I have provided notes on 
what is different from the book examples and why. 

**You should absolutely read the book and build each solution from scratch as you go.** Your solution may vary from
mine.

The following chapters all build from this base solution so it contains two projects:
- A class library project called `Money` which contains the code we are going to write.
- A test project called `Money.Tests` which contains the tests for the `Money` project

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
To set up the repository, you need to have .NET SDK installed on your machine. You can download it from
[here](https://dotnet.microsoft.com/download) or if you are on Mac you can use Homebrew:
```bash
brew install --cask dotnet-sdk
```

Once you have .NET SDK installed, you can clone this repository and open it in your favorite IDE. I use JetBrains Rider, 
but you can also use Visual Studio or Visual Studio Code (VSCode). 

_**Note:** I have not tested this repository in Visual Studio or VSCode_ as I don't use PC's for development and VSCode 
is not a professional quality IDE. 😲😜 (There. I said it.)

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS)versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
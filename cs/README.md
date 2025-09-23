# "Test-Driven Development: By Example" in C#

This folder contains C# implementations of the examples from the book "Test-Driven Development: By Example" by Kent 
Beck. The examples cover various aspects of Test-Driven Development (TDD) and demonstrate how to apply TDD principles in
C#.

## Contents
I already described the contents of this repository in the [main README](../README.md#structure-of-the-repository), so 
please refer to that for more information about the examples included here.

## Getting Started
I'm going to assume you have some familiarity with C# and .NET development and that you have a development environment
(JetBrains Rider, Visual Studio, or VSCode) set up for C#. 
To get started with the examples in this repository, follow these steps:
1. Install .NET SDK 8.0 or later: Make sure you have the .NET SDK installed on your machine. You can download it from 
the official [.NET website](https://dotnet.microsoft.com/download) or use a package manager like Homebrew (for MacOS) to 
install it.
2. You will also want the .NET CLI tools for running tests and managing projects. These are included with the .NET SDK.
3. Create a working directory for yourself and clone this repository to your local machine using Git:
```bash
    git clone https://github.com/The-Software-Gorilla/tdd-by-example.git
 ```
This will pull down the entire repository, including the C# and Java examples.
4. Navigate to the `cs` directory and one of the chapter directories to explore the code examples.
5. Open the solution file (`.sln`) in your preferred C# IDE to explore the code and run the tests.
6. You can run the tests using your IDE's built-in test runner or by using the .NET CLI. To run the tests from the
command line, navigate to the test project directory and execute:
```bash
    dotnet test
```
7. In Rider, you can also right-click on the `Money.Tests` project and select "Run Unit Tests" to execute the tests.

## Code Structure
The C# examples are organized into separate directories, each corresponding to a chapter or section of the book. Each 
directory contains a C# solution file (`.sln`). You can open these solution files in your preferred C# IDE (like 
JetBrains Rider, Visual Studio, or VSCode) to explore the code and run the tests.

The code is in two separate projects within each solution:
- `Money`: The main project contains the implementation code.
- `Money.Tests`: The test project contains the unit tests for the implementation code.

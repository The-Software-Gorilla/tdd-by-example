# "Test-Driven Development: By Example" in Java

This folder contains Java implementations of the examples from the book "Test-Driven Development: By Example" by Kent
Beck. The examples cover various aspects of Test-Driven Development (TDD) and demonstrate how to apply TDD principles in
C#.

## Contents
I already described the contents of this repository in the [main README](../README.md#structure-of-the-repository), so
please refer to that for more information about the examples included here.

## Getting Started
I'm going to assume you have some familiarity with Java development and that you have a development environment
(JetBrains IntelliJ IDEA Ultimate, Eclipse, or VSCode) set up for Java.
To get started with the examples in this repository, follow these steps:
1. Install Java JDK 24 or later: Make sure you have the JDK installed on your machine. You can download it from
   the official [OpenJDK site](https://jdk.java.net/25/) or use a package manager like Homebrew (for MacOS) to
   install it.
2. You will also need to install Maven tools for running tests and building projects. You can find installation
   instructions on the [Maven website](https://maven.apache.org/install.html) or use Homebrew on MacOS (**highly 
recommended**):
    ```bash
        brew install maven
    ```

3. Create a working directory for yourself and clone this repository to your local machine using Git:

    ```bash
        git clone https://github.com/The-Software-Gorilla/tdd-by-example.git
     ```
    
    This will pull down the entire repository, including the C# and Java examples.
4. Navigate to the `java` directory and one of the chapter directories to explore the code examples.
5. Open the directory in your preferred Java IDE to explore the code and run the tests.
6. You can run the tests using your IDE's built-in test runner or by using Maven from the command line. To run the tests 
from the command line, navigate to the test project directory and execute:
    ```bash
        mvn clean test
    ```
7. In IntelliJ, you can also right-click on the `src/test/java` folder and select "Run 'Tests in 'java'' to execute the 
tests.

## Code Structure
The Java examples are organized into separate directories, each corresponding to a chapter or section of the book. Each
directory contains two folder structure for the code:
- `pom.xml`: The Maven Project Object Model (POM) file that contains the project configuration, dependencies, and build
  settings.
- `README.md`: A README file that provides an overview of the chapter and any specific instructions or notes related to
  the code in that chapter.
- `src/main/java`: The main project contains the implementation code.
- `src/test/java`: The test project contains the unit tests for the implementation code.
Under those folders, code is organized by package. 

The package structure for this solution is 
`com.thesoftwaregorilla.tdd.money`.

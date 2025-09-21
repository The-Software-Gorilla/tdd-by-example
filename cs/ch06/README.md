# Test-Driven Development - C# - Chapter 06

This solution contains code for Chapter 6 of the book ["Test-Driven Development By Example" by 
Kent Beck](https://a.co/d/1sr05eT). The code is written in C# and uses the NUnit testing framework for the tests. 

For information on how to set up the repository, please see the [README in the ch00](../ch00/README.md) folder.

## Chapter 6 - Equality for All, Redux
The outcome of this chapter is relatively minor, but the real value is working through the process that Kent goes 
through getting to the end state. That's been true of some of the earlier chapters too, but this is the point where
TDD really starts showing its value because you're fundamentally changing the design of the code.

A key outcome for this chapter is that I moved all the `//TODO` items to the `MoneyTest` class because that's where
they'll ultimately be implemented. `Dollar` and `Franc` will be removed.

### TODO list at the end of the chapter
By the end of the chapter, the TODO list looks like this:
- [ ] \$5 + 10 CHF = $10 if rate is 2:1
- [x] \$5 * 2 = $10
- [x] Make "amount" private
- [x] Dollar side-effects?
- [ ] Money rounding?
- [x] equals()
- [ ] hashCode()
- [ ] Equal null
- [ ] Equal object
- [x] 5 CHF * 2 = 10 CHF
- [ ] Dollar/Franc duplication
- [x] Common Equals
- [ ] Common Times
- [ ] Compare Francs with Dollars

## Last Update
I try and keep this code up to date with the latest versions. I generally wait until a new version of .NET SDK is 
released and I only update it for Long Term Support (LTS)versions. .NET 8 is the latest LTS version as of this writing.

This repository was last updated in September 2025.
- .NET SDK version 8
- NUnit version 4.4.0
- JetBrains Rider version 2025.2.2
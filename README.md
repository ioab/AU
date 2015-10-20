Algorithms Unlocked Implementation
==================================


###Description
A C#.NET implementation of (algorithms unlocked) by Prof. Thomas H. Cormen.

[(book page)](https://mitpress.mit.edu/books/algorithms-unlocked)


###Demo
Demo project serves as a sample testing for the algorithms library code.
Demo.cs is a gate to the divided testing modules.

* Sample Function (finite automaton string matching):

```C#
var nextState = Matching.BuildNextStateTable("AAC");

foreach (var match in Matching.FAStringMatcher("GTAACAGTAAACG", 13, 3, nextState))
    Console.WriteLine(match);
```


###TODO
* "Data Compression" chapter.

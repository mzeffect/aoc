# Advent of Code

Solutions for [Advent of Code](https://adventofcode.com).

## Structure

```
aoc/
├── Utils.fs              # Shared utilities
├── Year2025/             # Solutions for 2025
│   ├── Day01a.fs
│   ├── Day01b.fs
│   └── ...
└── data/
    └── 2025/
        ├── Day01/
        │   ├── input.txt
        │   └── examples/
        │       ├── 01a.txt
        │       ├── 01a_out.txt
        │       ├── 01b.txt
        │       └── 01b_out.txt
        └── ...
```

### Prerequisites

- .NET SDK 10 ([instructions](https://dotnet.microsoft.com/en-us/download))

### How to run

Install dependencies:

```shell
dotnet restore
```

Run the first part of the puzzle for a specific day, use the `a` suffix:

```shell
dotnet run 2025/Day01a
```

To run the second part, use `b`:

```shell
dotnet run 2025/Day01b
```

You can also omit the year if there's no ambiguity:

```shell
dotnet run Day01a
```

### Add a new solver

As an example, here's how to add a new solver for day 13, first part:

1. Add a new F# file in the year folder, e.g. `Year2025/Day13a.fs`
2. Create a new folder in `/data/2025`, e.g. `/data/2025/Day13`
3. Your puzzle input should be in `/data/2025/Day13/input.txt`
4. Put the puzzle's example input into `/data/2025/Day13/examples/13a.txt`
5. Put the expected output for the example into `/data/2025/Day13/examples/13a_out.txt`

The examples act as tests, they will be passed to the solver and the result will be compared to the expected output.

You will be notified if your code fails to pass on the examples.

You can also use `./bootstrap.sh 2025/Day01a` to create the required files.

### Write the solver

A solver is a function that takes a string (input) and returns a string (solution).

It has to be called `solve` and must be in a module with the same name as the day + puzzle part, e.g. `Day13a`. This is because the runner uses reflection to find the solver for each puzzle.

```fsharp
module AdventOfCode.Year2025.Day13a

open AdventOfCode.Utils

let solve (input: string) =
    "implement me"
```

Of course, you can structure your code however you want, but the runner will only look for and execute the `solve` function.

### Adding a new year

To add solutions for a new year:

1. Create a new folder, e.g. `Year2026/`
2. Create a new data folder, e.g. `data/2026/`

The shared utils module can be used across all years.

module AdventOfCode.Year2025.Day03a

open AdventOfCode.Utils

let maxPossibleJoltage (a: int[]) =
    let maxIdx = Array.length a - 1
    let mutable maxVal = 0
    let mutable secondMaxVal = 0
    a |> Array.iteri (fun i v ->
        if v > maxVal && i <> maxIdx then
            maxVal <- v
            secondMaxVal <- 0
        elif v > secondMaxVal then
            secondMaxVal <- v
    )
    string maxVal + string secondMaxVal |> int

let solve (input: string) =
    input |> splitLines |> Seq.map (splitToIntArray >> maxPossibleJoltage) |> Seq.sum |> string

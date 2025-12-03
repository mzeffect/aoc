module AdventOfCode.Year2025.Day03b

open System
open AdventOfCode.Utils

let maxPossibleJoltage (targetCount: int) (a: int[]) =
    let maxIndex = Array.length a - 1
    let mutable vals = Array.zeroCreate targetCount

    let mutable v = -1
    let mutable placed = false
    let mutable rank = 0

    for i in 0 .. maxIndex do
        v <- a.[i]
        placed <- false
        rank <- 0

        while not placed && rank < targetCount do
            if v > vals.[rank] && maxIndex - i >= targetCount - rank - 1 then
                vals.[rank] <- v
                if rank < targetCount - 1 then
                    Array.fill vals (rank + 1) (targetCount - rank - 1) 0
                placed <- true
            else
                rank <- rank + 1
    
    String.Join("", vals) |> int64

let solve (input: string) =
    input |> splitLines |> Seq.map (splitToIntArray >> maxPossibleJoltage 12) |> Seq.sum |> string

module AdventOfCode.Year2025.Day01b

open AdventOfCode.Utils
open AdventOfCode.Year2025.Day01a

let rotate (pos: int, rotation: Rotation) =
    let getNextPos =
        match rotation with
        | Left, _ -> fun p -> p - 1
        | Right, _ -> fun p -> p + 1

    let _, steps = rotation
    let mutable zeroHits = 0
    let mutable currentPos = pos

    for _ in 1..steps do
        let nextPos = getNextPos currentPos

        if nextPos = -1 then currentPos <- 99
        elif nextPos = 100 then currentPos <- 0
        else currentPos <- nextPos

        if currentPos = 0 then
            zeroHits <- zeroHits + 1

    currentPos, zeroHits

let solve (input: string) =
    input
    |> parseInput
    |> Seq.fold
        (fun (pos, zeroHits) rotation ->
            let newPos, hitCount = rotate (pos, rotation)
            newPos, zeroHits + hitCount)
        (50, 0)
    |> snd
    |> string

module AdventOfCode.Year2025.Day01a

open AdventOfCode.Utils

type Rotation = Dir * int

let parseRotation (raw: string) =
    let steps = System.Int32.Parse raw.[1..]
    if raw.[0] = 'L' then Left, steps else Right, steps

let parseInput (input: string) =
    input |> splitLines |> Array.map parseRotation

let modulus = 100 // 0..99

let rotate (pos: int, rotation: Rotation) =
    match rotation with
    | Left, steps -> ((pos - steps) % modulus + modulus) % modulus
    | Right, steps -> (pos + steps) % modulus

let solve (input: string) =
    input
    |> parseInput
    |> Seq.fold
        (fun (pos, zeroHits) rotation ->
            let newPos = rotate (pos, rotation)
            newPos, if newPos = 0 then zeroHits + 1 else zeroHits)
        (50, 0)
    |> snd
    |> string

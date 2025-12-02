module AdventOfCode.Year2025.Day02b

open AdventOfCode.Utils

let isInvalidId (id: int64) =
    let s = string id
    let maxRepeatingLength = greatestDivisor (String.length s)

    match maxRepeatingLength with
    | None -> false
    | Some l ->
        seq { l .. -1 .. 1 }
        |> Seq.filter (fun length -> s.ToCharArray() |> Seq.chunkBySize length |> Seq.distinct |> Seq.length = 1)
        |> Seq.isEmpty
        |> not

let solve (input: string) =
    input
    |> splitWordsBy ','
    |> Seq.map (splitWordsBy '-' >> Array.map int64)
    |> Seq.fold
        (fun (invalidCount) boundaries ->
            invalidCount
            + (seq { boundaries.[0] .. boundaries.[1] } |> Seq.filter isInvalidId |> Seq.sum))
        0L
    |> string

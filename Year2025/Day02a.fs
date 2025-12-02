module AdventOfCode.Year2025.Day02a

open AdventOfCode.Utils

let isInvalidId (id: int64) =
    let s = string id

    if String.length s % 2 = 0 then
        let mid = String.length s / 2
        s.[.. mid - 1] = s.[mid..]
    else
        false

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

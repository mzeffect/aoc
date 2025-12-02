module AdventOfCode.Year2025.Day02b

open AdventOfCode.Utils
open System.Text.RegularExpressions

// Compile regex once
let private invalidIdRegex = Regex(@"^(.+)\1+$", RegexOptions.Compiled)

let isInvalidId id =
    invalidIdRegex.IsMatch(string id)

let solve (input: string) =
    input
    |> splitWordsBy ','
    |> Seq.map (splitWordsBy '-')
    |> Seq.sumBy (fun boundaries ->
        seq { int64 boundaries.[0] .. int64 boundaries.[1] } 
        |> Seq.filter isInvalidId 
        |> Seq.sum)
    |> string

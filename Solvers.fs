module AdventOfCode.Solvers

open System.Reflection
open System.Text.RegularExpressions

type SolverInfo = {
    Year: string
    DayPart: string
    Method: MethodInfo
}

let getSolvers () =
    let assembly = Assembly.GetExecutingAssembly()

    let solverTypes =
        assembly.GetTypes()
        |> Array.filter (fun t ->
            t.Namespace <> null &&
            t.Namespace.StartsWith("AdventOfCode.Year") &&
            t.Name.StartsWith("Day"))

    solverTypes
    |> Array.collect (fun t ->
        let yearMatch = Regex.Match(t.Namespace, @"Year(\d{4})")
        if yearMatch.Success then
            let year = yearMatch.Groups.[1].Value
            t.GetMethods(BindingFlags.Public ||| BindingFlags.Static)
            |> Array.filter (fun m -> m.Name = "solve")
            |> Array.map (fun m ->
                t.Name, { Year = year; DayPart = t.Name; Method = m })
        else
            [||])
    |> Map.ofArray

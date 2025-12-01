open System.Diagnostics
open System.IO
open System.Threading
open AdventOfCode.Solvers

let solvers = getSolvers ()

let args = System.Environment.GetCommandLineArgs()
let runArg = if args.Length > 1 then args.[1] else ""

let runSolver (fullArg: string) =
    // Parse argument: either "2025/Day01a" or "Day01a"
    let (year, dayArg) =
        if fullArg.Contains("/") then
            let parts = fullArg.Split('/')
            (parts.[0], parts.[1])
        else
            // Find solver and extract year from it
            let solverInfo = solvers[fullArg]
            (solverInfo.Year, fullArg)

    let solverInfo = solvers[dayArg]

    // Verify the year matches
    if year <> solverInfo.Year then
        printfn $"Error: Solver {dayArg} is for year {solverInfo.Year}, but you requested year {year}"
        exit 1

    let dayId = dayArg.Substring(0, dayArg.Length - 1)
    let puzzleId = dayArg.Substring(3)

    let solve = fun input -> solverInfo.Method.Invoke(null, [| input |]) :?> string

    let input =
        try
            File.ReadAllText($"data/{year}/{dayId}/input.txt")
        with :? FileNotFoundException ->
            printfn $"Input file for {year}/{dayId} not found"
            ""

    // run once so solver gets JIT'd before benchmarking
    solve input |> ignore

    if runArg <> "runAll" then
        let examplesPath = $"data/{year}/{dayId}/examples"

        if Directory.Exists(examplesPath) then
            let exampleFiles =
                Directory.GetFiles(examplesPath)
                |> Array.filter (fun f -> (not (f.EndsWith "_out.txt")))
                |> Array.filter (fun f -> f.Contains puzzleId)

            if exampleFiles.Length > 0 then
                printfn "Testing with examples..."

                exampleFiles
                |> Array.iter (fun f ->
                    let input = File.ReadAllText(f)
                    let output = solve input
                    let expectedOutput = File.ReadAllText(f.Replace(".txt", "_out.txt"))

                    if output <> expectedOutput.TrimEnd() then
                        printfn $"❌  Example {f} failed"
                        printfn $"Expected: {expectedOutput.TrimEnd()}"
                        printfn $"Actual: {output}"
                    else
                        printfn $"✅  Example {f} passed")

        if input.Length = 0 then
            // no input file found, exit
            exit 1

        printfn "Solution for puzzle input:"
        let sw = Stopwatch.StartNew()
        printfn $"{solve input}"
        sw.Stop()
        printfn $"Solver runtime <{dayArg}>: {sw.Elapsed.TotalMilliseconds}ms"
    else
        Thread.Sleep(100)
        let sw = Stopwatch.StartNew()
        for i = 1 to 10 do
            solve input |> ignore
        sw.Stop()
        printfn $"Average runtime <{dayArg}>: {sw.Elapsed.TotalMilliseconds / float 10}ms"

if runArg = "" then
    printfn "Usage: dotnet run <year>/<DayXXy>"
    printfn "Example: dotnet run 2025/Day01a"
    printfn ""
    printfn "You can also omit the year: dotnet run Day01a"
    exit 1
elif runArg = "runAll" then
    for solver in solvers do
        runSolver solver.Key
else
    runSolver runArg

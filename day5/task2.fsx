open System
#load "../Utils.fsx"
open Utils

let lines = read (__SOURCE_DIRECTORY__ + "/input.txt")

type Command = {
    fromPos: int * int;
    toPos: int * int;
}

let printBoard (board: int[,]) =
    let builder = new System.Text.StringBuilder()
    for x in 0 .. board.GetUpperBound(0) do
        for y in 0 .. board.GetUpperBound(1) do
            builder.Append($"{board.[x,y]} ") |> ignore
        builder.AppendLine() |> ignore

    builder |> dumpf

let parseCommand (line: string) =
    let coords (seg: string) = seg.Split(",") |> Array.map int
    
    let segments = line.Split(" -> ")    
    let leftCoords = coords segments.[0]
    let rightCoords = coords segments.[1]

    { fromPos = (leftCoords.[0], leftCoords.[1])
      toPos = (rightCoords.[0], rightCoords.[1]) }

let commands = lines |> Array.map parseCommand

let maxXFromCommand (command: Command) =
    let x1, _ = command.fromPos
    let x2, _ = command.toPos
    Math.Max(x1, x2)

let maxYFromCommand (command: Command) =
    let _, y1 = command.fromPos
    let _, y2 = command.toPos
    Math.Max(y1, y2)

let maxX = commands |> Array.map maxXFromCommand |> Array.max
let maxY = commands |> Array.map maxYFromCommand |> Array.max

let board = Array2D.zeroCreate<int> (maxX + 2) (maxY + 2)

let (|Horizontal|_|) (command: Command) =
    match (command.fromPos, command.toPos)  with
    | (x1, y1), (x2, y2) when x1 = x2 
        -> Some(x1, (min y1 y2), (max y1 y2))
    | _ -> None

let (|Vertical|_|) (command: Command) =
    match (command.fromPos, command.toPos)  with
    | (x1, y1), (x2, y2) when y1 = y2 
        -> Some(y1, (min x1 x2), (max x1 x2))
    | _ -> None

let (|Diagonal|_|) (command: Command) =
    match (command.fromPos, command.toPos)  with
    | (x1, y1), (x2, y2) when y1 <> y2  && x1 <> x2
        -> Some(((x1, y1), (x2, y2)))        
    | _ -> None

let fill (command: Command) =
    match command with
    | Horizontal(x, y1, y2) -> 
        for y in y1 .. y2 do
            board.[x, y] <- board.[x, y] + 1
    | Vertical(y, x1, x2) ->
        for x in x1 .. x2 do
            board.[x, y] <- board.[x, y] + 1
    | Diagonal((x1, y1), (x2, y2)) ->
        let diff = abs (x1 - x2)
        let xIncr = if x1 < x2 then 1 else -1
        let yIncr = if y1 < y2 then 1 else -1
        printf $"({x1},{y1})->({x2},{y2}) diff {diff} xincr {xIncr} yincr {yIncr}\n"
        for i in 0 .. diff do
            let x = x1 + (xIncr * i)
            let y = y1 + (yIncr * i)
            printfn $"{x}, {y}\n"
            board.[x, y] <- board.[x, y] + 1
        ()
    | _ -> ()

commands |> Array.map fill

printBoard board

seq {
    for x in 0 .. board.GetUpperBound(0) do
        for y in 0 .. board.GetUpperBound(1) do
            if board.[x, y] > 1 then 
                yield board.[x, y]

} 
|> Seq.length 
|> dump

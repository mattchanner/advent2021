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

let (|Coords|_|) (command: Command) =
    match (command.fromPos, command.toPos)  with
    | (x1, y1), (x2, y2) when x1 = x2 || y1 = y2 -> Some((min x1 x2, min y1 y2), (max x1 x2, max y1 y2))
    | _ -> None

let fill (command: Command) =
    match command with
    | Coords((x1, y1), (x2, y2)) -> 
        printf $"Line ({x1},{y1}) to ({x2},{y2})\n"
        if x1 = x2 then            
            for y in y1 .. y2 do
                board.[x1, y] <- board.[x1, y] + 1
        else
            for x in x1 .. x2 do
                board.[x, y1] <- board.[x, y1] + 1
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

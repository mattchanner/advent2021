open System
open System.IO
open System.Text

#load "../Utils.fsx"

open Utils

let isComplete (board: int[,]) =
    let isColumnComplete col =        
        let mutable allComplete =  true
        for y in 0 .. board.GetUpperBound(1) do
            if board.[col, y] >= 0 then    
                allComplete <- false
        allComplete
    let isRowComplete row =
        let mutable allComplete =  true
        for x in 0 .. board.GetUpperBound(0) do
            if board.[x, row] >= 0 then    
                allComplete <- false
        allComplete
    
    let complete = 
        [|0..board.GetUpperBound(0)|] 
            |> Array.map isColumnComplete 
            |> Array.exists (fun x -> x)
        ||
        [|0..board.GetUpperBound(1)|] 
            |> Array.map isRowComplete 
            |> Array.exists (fun x-> x)
    complete

let play (board: int[,]) value =
    for x in 0 .. board.GetUpperBound(0) do
        for y in 0 .. (board.GetUpperBound(1)) do
            if board.[x, y] = value then
                board.[x, y] <- -1
    isComplete board

let remaining board =
    let mutable sum = 0
    board |> Array2D.iter (fun x -> if x > 0 then sum <- sum + x)
    sum

let parseBoardAt (array: string[]) line =
    let board = Array2D.zeroCreate<int> 5 5
    let mutable row = 0
    for i in line .. (line + 4) do
        let cells = array.[i].Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int
        for x in 0 .. cells.Length - 1 do
            board.[row, x] <- cells.[x]
        row <- row+ 1
    board

let rec parseBoardMarkers (array: string[]) line (boards: int[,] list): int[,] list =
    if line >= array.Length then
        printfn "%A" $"Returning boards {boards}\n\n"
        boards
    else
        let board = parseBoardAt array line
        let boards = parseBoardMarkers array (line + 5) (board::boards)
        printf $"Returning boards {boards}\n\n"
        boards

let playAll (answers: int[]) boards = 
    let mutable matched = Array2D.zeroCreate<int> 0 0
    let mutable answer = 0
    for i in 0 .. answers.Length do
        for board in boards do
            if answer = 0 && play board (answers.[i]) then
                matched <- board
                answer <- answers.[i]
    (remaining matched) * answer

let lines = read (__SOURCE_DIRECTORY__  + "/input.txt")

let answers = lines.[0].Split(",", StringSplitOptions.RemoveEmptyEntries) |> Array.map int

parseBoardMarkers lines 1 []
|> playAll answers
|> dump





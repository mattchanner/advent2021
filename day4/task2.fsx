open System
open System.IO
open System.Text

#load "../Utils.fsx"

open Utils

let identity x = x

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
            |> Array.exists identity
        ||
        [|0..board.GetUpperBound(1)|] 
            |> Array.map isRowComplete 
            |> Array.exists identity
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
        if array.Length > i then
            let cells = array.[i].Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int
            for x in 0 .. cells.Length - 1 do
                board.[row, x] <- cells.[x]
            row <- row+ 1
    board

let rec parseBoardMarkers (array: string[]) line (boards: int[,] list): int[,] list =
    if line >= array.Length then
        boards
    else
        let board = parseBoardAt array line
        let boards = parseBoardMarkers array (line + 6) (board::boards)
        boards

let winningBoards (answers: int[]) (boards: int[,] list) =
    seq {
        let mutableBoards = new ResizeArray<int[,]>(boards |> List.rev)
        for answer in answers do
            let iterCopy = mutableBoards.ToArray()         
            for board in iterCopy do
                if play board answer then
                    mutableBoards.Remove(board) |> ignore
                    yield ((remaining board) * answer)
    }

let lines = read (@"C:\Source\github\mattchanner\advent2021\day4\input.txt")

let answers = lines.[0].Split(",", StringSplitOptions.RemoveEmptyEntries) |> Array.map int

parseBoardMarkers lines 2 []
|> winningBoards answers
|> Seq.rev
|> Seq.item 0
|> dump
|> ignore
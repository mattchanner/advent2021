open System
open System.IO

let read filename = File.ReadAllLines(filename) 

let dump what = 
    printfn "%A" what
    what

let pivotArray a = 
    a 
    |> Array.collect Array.indexed
    |> Array.groupBy fst
    |> Array.map (snd >> Array.map snd)

let binToInt bin = Convert.ToInt32(bin, 2)

let bitToChar bits = Array.map (fun bit -> if bit = 0 then '0' else '1') bits

let charsToString chars = 
    Array.ofSeq chars |> String

let dumpBinaryStrings arr = 
    arr 
    |> Array.map bitToChar 
    |> Array.map charsToString 
    |> dump
open System
open System.IO

let read filename = File.ReadAllLines(filename) 

let dump what = 
    printfn "%A" what
    what

let transpose a = 
    a 
    |> Seq.collect Seq.indexed
    |> Seq.groupBy fst
    |> Seq.map (snd >> Seq.map snd)

let binToInt bin = Convert.ToInt32(bin, 2)

let charsToString chars = 
    Array.ofSeq chars |> String
open System
open System.IO

let dump what = 
    printfn "%A" what
    what

let transpose a = 
    a 
    |> Seq.collect Seq.indexed
    |> Seq.groupBy fst
    |> Seq.map (snd >> Seq.map snd)

let charsToString chars = 
    Array.ofSeq chars |> String

let binToInt bin = Convert.ToInt32(bin, 2)

let lines = File.ReadAllLines("input.txt")
let entries = Array.length lines

let counts = 
    lines
    |> Seq.ofArray
    |> Seq.map (fun x -> x.ToCharArray())
    |> transpose
    |> Seq.map (fun x -> Seq.filter (fun p -> p = '1') x |> Seq.length)
    |> Seq.map (fun x -> (x, entries - x))
    
let gamma = 
    counts 
    |> Seq.map (fun x -> if (fst x) > (snd x) then '1' else '0') 
    |> charsToString 
    |> binToInt

let epsilon = 
    counts 
    |> Seq.map (fun x -> if (fst x) > (snd x) then '0' else '1') 
    |> charsToString 
    |> binToInt

gamma |> dump
epsilon |> dump

gamma * epsilon |> dump
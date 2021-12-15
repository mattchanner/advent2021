open System
open System.IO

#load "../Utils.fsx"

open Utils

let lines = read "input.txt"
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
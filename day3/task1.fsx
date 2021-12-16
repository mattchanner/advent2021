open System
open System.IO

#load "../Utils.fsx"

open Utils

let lines = read "input.txt"
let entries = Array.length lines

let counts = 
    lines
    |> Array.map (fun x -> x.ToCharArray())
    |> pivotArray
    |> Array.map (fun x -> Array.filter (fun p -> p = '1') x |> Array.length)
    |> Array.map (fun x -> (x, entries - x))
    
let gamma = 
    counts 
    |> Array.map (fun x -> if (fst x) > (snd x) then '1' else '0') 
    |> charsToString 
    |> binToInt

let epsilon = 
    counts 
    |> Array.map (fun x -> if (fst x) > (snd x) then '0' else '1') 
    |> charsToString 
    |> binToInt

gamma |> dump
epsilon |> dump

gamma * epsilon |> dump


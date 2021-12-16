open System
open System.IO

#load "../Utils.fsx"

open Utils

let lines = read "input.txt"

// The array of binary numbers (treated as an array or 0's and 1's)
let ungrouped = 
    lines 
    |> Array.map (fun x -> 
        x.ToCharArray() 
        |> Array.map (fun x -> if x = '0' then 0 else 1)
    )

// Filters the ungrouped numbers by removing elements where a bit does not match at
// a given position
let filterNumbers (numbers: int[][]) pos bit = 
    numbers |> Array.filter (fun x -> x.[pos] = bit)

let chooser arr f =
    let len = Array.length arr
    let zeros = Array.filter (fun x -> x = 0) arr |> Array.length
    let ones = len - zeros 
    f ones zeros

// Most common value (0 or 1) in the array. If 0 and 1 are equally common, keep values with a 1
let mcb arr = chooser arr (fun ones zeros -> if ones >= zeros then 1 else 0)

// Least common value (0 or 1) in the array. If 0and 1 are equally common, return 0
let lcb arr = chooser arr (fun ones zeros -> if ones >= zeros then 0 else 1)
    
let rec filterSourceArray source func i =
    let pivot = pivotArray source
    let filtered = filterNumbers source i (func pivot.[i])    
    if filtered.Length = 1 then 
        filtered.[0]
    else 
        filterSourceArray filtered func (i + 1)

let algorithm selector =
    filterSourceArray ungrouped selector 0 
    |> bitToChar 
    |> charsToString 
    |> binToInt

let oxygenGeneratorRating = filterSourceArray ungrouped mcb 0
let co2ScrubberRating = filterSourceArray ungrouped lcb 0

let oxygen = algorithm mcb
let co2 = algorithm lcb

dump oxygen
dump co2
dump (oxygen * co2)
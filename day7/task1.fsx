#load "../Utils.fsx"
open Utils

//let crabs = "16,1,2,0,4,2,7,1,2,14".Split(",") |> Array.map int
let crabs = 
    read (__SOURCE_DIRECTORY__ + "/input.txt" )
    |> Array.item 0
    |> fun c -> c.Split(",") 
    |> Array.map int

let max = Array.max crabs

let costAt pos =
    crabs 
    |> Array.map (fun crab -> abs (pos - crab))
    |> Array.sum
        

let median = crabs |> Array.sort |> Array.item (Array.length crabs / 2)
printf $"Median is {median}"

let cost = costAt median
printf "Cost is {cost}"
#load "../Utils.fsx"
open Utils

//let crabs = "16,1,2,0,4,2,7,1,2,14".Split(",") |> Array.map int
let crabs = 
    read (__SOURCE_DIRECTORY__ + "/input.txt" )
    |> Array.item 0
    |> fun c -> c.Split(",") 
    |> Array.map int

let cumulativeFuelCost distance =
    let mutable x = 0
    for i in 1 .. distance do
        x <- x + i
    x

let costAt pos =
    crabs 
    |> Array.map (fun crab -> 
        let diff = abs (pos - crab)
        let cost = cumulativeFuelCost diff
        //printf $"Move from {crab} to {pos} {cost}\n"
        cost
    )
    |> Array.sum
        

let mutable minCost = System.Int32.MaxValue
let maxCrab = Array.max crabs
let mutable minI = -1
for i in 1 .. maxCrab do
    let cost = costAt i
    if cost < minCost then 
        minCost <- cost
        minI <- i

printf $"Min fuel cost {minCost} to position {minI}\n"
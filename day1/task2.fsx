open System.IO

#load "../Utils.fsx"

open Utils

read "input.txt"
|> Array.map int
|> (Array.windowed 3)
|> Array.map (fun x -> Array.fold (+) 0 x)
|> (Array.windowed 2)
|> Array.filter (fun x -> x.[1] > x.[0])
|> Array.length
|> dump

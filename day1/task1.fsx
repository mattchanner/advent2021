open System.IO

#load "../Utils.fsx"

open Utils

read "input.txt"
|> Array.map int
|> (Array.windowed 2)
|> Array.filter (fun x -> x.[1] > x.[0])
|> Array.length
|> dump

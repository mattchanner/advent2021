open System.IO

let dump what = 
    printfn "%A" what
    what

File.ReadAllLines(@"input.txt") 
|> Array.map int
|> (Array.windowed 2)
|> Array.filter (fun x -> x.[1] > x.[0])
|> Array.length
|> dump
|> ignore

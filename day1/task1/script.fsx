open System.IO

let dump what = 
    printfn "%A" what
    what

File.ReadAllLines("input.txt") 
|> (Array.windowed 2)
|> Array.filter (fun pair -> pair.[1] > pair.[0])
|> Array.length
|> dump
|> ignore

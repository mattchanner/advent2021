open System.IO

#load "../Utils.fsx"

open Utils

type Command =
   | Up of int
   | Down of int
   | Forward of int
   
type State = {
    h: int
    v: int
}

let parse (x:string) =
    match (x.Substring(0, x.Length - 2), int (x.Substring(x.Length - 1))) with 
    | "forward", amount -> Forward(amount)
    | "down", amount -> Down(amount)
    | "up", amount -> Up(amount)
    | _ -> failwith (sprintf "Unknown command %s" x)
    
let apply (state:State) (command:Command) =
    match command with
    | Up(amount) -> { state with v = state.v - amount }
    | Down(amount) -> { state with v = state.v + amount }
    | Forward(amount) -> { state with h = state.h + amount }

let initial = { h = 0; v = 0 }

read "input.txt"
|> Array.map parse
|> Array.fold apply initial
|> fun x -> x.h * x.v
|> dump
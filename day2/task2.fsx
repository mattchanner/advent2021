open System.IO

let dump what = 
    printfn "%A" what
    what
    
type Command =
   | Up of int
   | Down of int
   | Forward of int
   
type State = {
    h: int
    v: int
    aim: int
}

let parse (x:string) =
    match (x.Substring(0, x.Length - 2), int (x.Substring(x.Length - 1))) with 
    | "forward", amount -> Forward(amount)
    | "down", amount -> Down(amount)
    | "up", amount -> Up(amount)
    | _ -> failwith (sprintf "Unknown command %s" x)
    
let apply (state:State) (command:Command) =
    match command with
    | Down(amount) -> { state with aim = state.aim + amount }
    | Up(amount) -> { state with aim = state.aim - amount }    
    | Forward(amount) -> { state with h = state.h + amount; v = (state.v + (state.aim * amount)) }

let initial = { h = 0; v = 0; aim = 0 }

File.ReadAllLines(@"input.txt") 
|> Array.map parse
|> Array.fold apply initial
|> fun x -> x.h * x.v
|> dump
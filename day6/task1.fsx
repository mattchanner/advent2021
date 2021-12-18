#load "../Utils.fsx"
open Utils

type Lanternfish = {
    timer: int
} with 
    member this.Tick() =
        if this.timer = 0 then
            [|{ timer = 6 }; { timer = 8 };|]
        else
            [|{ timer = this.timer - 1 }|]
    override this.ToString() = this.timer |> string


let school = 
    read (__SOURCE_DIRECTORY__ + "/input.txt" )
    |> Array.item 0
    |> fun c -> c.Split(",") 
    |> Array.map int 
    |> Array.map (fun x -> { timer = x })

let printDay day (fish: Lanternfish seq) =
    printf $"Day {day}: Count: {Seq.length fish}"
    //printf "%A" (fish |> Seq.map (fun x -> x.ToString()) |> Seq.reduce (fun a b -> a + b))
    printf "\n"


let rec iter day (school: Lanternfish seq): unit =
    if day <= 80 then
        printDay day school
        let next = seq {
            for fish in school do
                for next in fish.Tick() do
                    yield next
        }
        iter (day + 1) next
    ()   

printf "\n"
        
iter 0 school
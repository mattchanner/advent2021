#load "../Utils.fsx"
open Utils

let school = Array.zeroCreate<int64> 9

read (__SOURCE_DIRECTORY__ + "/input.txt" )
|> Array.item 0
|> fun c -> c.Split(",") 
|> Array.map int 
|> Array.iter (fun i -> Array.set school i ((Array.get school i) + 1l))

school |> dump
let printDay day =
    let schoolText = 
        school 
        |> Array.map string
        |> Array.reduce (fun x y -> $"{x} {y}")
    printf $"Day {day}: Count: {Array.sum school} | {schoolText}"
    printf "\n"


let rec iter day: unit =
    if day <= 256 then
        printDay day
        let zeros = school.[0]
        for i in 1 .. 8 do
            school.[i - 1] <- school.[i]
        school.[6] <- school.[6] + zeros
        school.[8] <- zeros
        iter (day + 1)
    ()   

printf "\n"
        
iter 0
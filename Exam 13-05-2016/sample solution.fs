open System

// Write a function that, given three numbers, returns
// - "first" if the first number is the greatest of all three;
// - "second" if the second number is the greatest of all three;
// - "third" if the third number is the greatest of all three.
let exercise1 : (int -> int -> int -> string) =
    fun first second third ->
        if first > second && first > third then "first"
        else if second > first && second > third then "second"
        else "third"


// Write a function "tau0" that takes an instance of Option<'a>
// and converts it into an instance of List<'a>. For example:
let tau0 : Option<'a> -> List<'a> =
    fun o ->
        match o with
        | None -> []
        | Some x -> [x]


// let List.collect = fun f l -> List.concat (List.map f l)
// Write a recursive function "tau1" with type (Option<'a> -> Option<'b>) -> List<'a> -> List<'b>.
let rec tau1 : (Option<'a> -> Option<'b>) -> List<'a> -> List<'b> =
    fun f -> List.collect (Some >> f >> tau0)


// A person is a record containing a name, a surname, and a date of birth. Define
// the date and person records and an "age" function that takes as input a person and
// returns the same person, older by one year.
type Date = { year:int; month:int; day:int }
type Person = { name:string; surname:string; dateOfBirth:Date }
let olderByOneDay : (Person -> Person) =
    fun p ->
        let newDateOfBirth = { p.dateOfBirth with year=p.dateOfBirth.year - 1 }
        { p with dateOfBirth=newDateOfBirth }

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code

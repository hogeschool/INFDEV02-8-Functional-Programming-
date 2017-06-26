open System

// [1 point] 1) define a function "collapse" that takes as input a pair of integers, and returns a triple with elements:
//  - the sum
//  - the difference
//  - the product
// of the two elements of the input.
let collapse : (int * int) -> (int * int * int) =
             fun (x,y) -> (x+y, x-y, x*y)

(*
collapse :: (Int,Int) -> (Int,Int,Int)
collapse (x,y) = (x+y, x-y, x*y)
*)

// [2 points] 2) define a recursive function "eval" that evaluates a boolean expression tree made up of
// boolean values,
// "or" of boolean expressions,
// "and" of boolean expressions.

type BooleanExpression =
    | Value of bool
    | Or of BooleanExpression * BooleanExpression
    | And of BooleanExpression * BooleanExpression

let rec eval : BooleanExpression -> bool =
             function
               | Value(x) -> x
               | Or(be_l:BooleanExpression, be_r:BooleanExpression) ->
                 eval be_l || eval be_r
               | And(be_l:BooleanExpression, be_r:BooleanExpression) ->
                 eval be_l && eval be_r

(*
data BooleanExpression = Value(Bool)
    | Or(BooleanExpression,BooleanExpression)
    | And(BooleanExpression,BooleanExpression)

eval :: BooleanExpression -> Bool
eval be =
    case be of
    | (Value x) -> x
    | (Or(a,b)) -> (eval a) || (eval b)
    | (And(a,b)) -> (eval a) && (eval b)

eval (Value x) = x
eval (Or(a,b)) = (eval a) || (eval b)
eval (And(a,b)) = (eval a) && (eval b)
*)

// Define a function reverse, which takes as input a triple and returns it reversed
let reverse : ('a * 'b * 'c) -> ('c * 'b * 'a) =
            fun (x:'a,y:'b,z:'c) -> (z,y,x)
// reverse :: (a,b,c) -> (c,b,a)
// reverse (x,y,z) = (z,y,x)

// Define a NonEmptyList<'a> data structure which can represent a list
// which can never be empty;
type NonEmptyList<'a> = Cons of 'a * List<'a>
// data NonEmptyList a = Cons(a, [a])

// Define a map function which maps a NonEmptyList<'a> to a NonEmptyList<'b>
// by means of a function 'a->'b;
let nonEmptyMap : NonEmptyList<'a> -> ('a->'b) -> NonEmptyList<'b> =
  fun l f ->
    match l with
    | Cons(h:'a,t:List<'a>) ->
        let (h':'b) = f(h)
        let (t':List<'b>) = List.map f t
        Cons(h',t')


// Write a function that, given two numbers, prints "Fizz"
// if the first number is divisible by 4,
// "Buzz" if the second is divisible by 4,
// "FizzBuzz" if both are divisible by 4.
let fizzBuzz : int -> int -> string =
  fun f s ->
    if f % 4 = 0 && s % 4 = 0 then "FizzBuzz"
    elif f % 4 = 0 then "Fizz"
    elif s % 4 = 0 then "Buzz"
    else ""

let rec zippp : List<'a> -> List<'b> -> List<'a*'b> =
    fun l1 l2 ->
        match l1,l2 with
        | [],[] -> []
        | (h1::t1),(h2::t2) -> (h1,h2) :: (zippp t1 t2)
        | _ -> failwith "Zippp only works on evenly long lists"

let rec unzippp : List<'a*'b> -> (List<'a>*List<'b>) =
    function [] -> ([],[])
            | ((h1,h2)::t) ->
              let (t1,t2) = unzippp t
              in ((h1::t1),(h2::t2))


// An arithmetic expression can be a number or sum, product, difference,
// or division of two arithmetic expressions. Define an arithmetic
// expression as a discriminate union and write a function that evaluates it.
type ArithExpr =
  | Const of int
  | Sum of (ArithExpr*ArithExpr)
  | Prod of (ArithExpr*ArithExpr)
let rec evalAE : ArithExpr -> int =
  fun ae ->
    match ae with
    | Const(i) -> i
    | Sum(l:ArithExpr,r:ArithExpr) -> (evalAE l) + (evalAE r)
    | Prod(l:ArithExpr,r:ArithExpr) -> (evalAE l) * (evalAE r)

let tau0 : Option<'a> -> List<'a> =
    fun o ->
      match o with
      | None -> []
      | Some x -> [x]

let tau1: (Option<'a> -> Option<'b>) -> List<'a> -> List<'b> =
    fun f_o l ->
        let l0 = List.map Some l
        let l1 = List.map f_o l0
        let l2 = List.map tau0 l1
        List.concat l2

let join : (Option<Option<'a>>) -> Option<'a> =
  fun o_o ->
    match o_o with
    | None -> None
    | Some(o:Option<'a>) ->
        match o with
        | None -> None
        | Some(x:'a) -> Some(x)


let rec joinnnn : List<List<'a>> -> List<'a> =
 fun l_l ->
    match l_l with
    | [] -> []
    | (l:List<'a>)::(ls:List<List<'a>>) ->
        match l with
        | [] -> joinnnn ls
        | (x:'a)::(xs:List<'a>) ->
            let (ls':List<List<'a>>) = xs::ls
            let (xs':List<'a>) = joinnnn(ls')
            x::xs'

let rec converttttt : List<Option<'a>> -> List<'a> =
    fun l_o ->
        let (l_l:List<List<'a>>) = List.map tau0 l_o
        joinnnn l_l


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code

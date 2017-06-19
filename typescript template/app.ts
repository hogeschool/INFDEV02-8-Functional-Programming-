import * as Option from './option'
import * as List from './list'

// INSERT ASSIGNMENT CODE HERE

console.log(Option.map(x => x + 1, Option.none<number>()))
console.log(Option.map(x => x + 1, Option.some<number>(1)))

console.log(List.map(x => x + 1, List.cons(1, List.cons(2, List.empty<number>()))))
console.log(List.filter(x => x % 2 == 0, List.cons(1, List.cons(2, List.empty<number>()))))

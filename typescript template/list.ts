export type List<A> = { kind:"empty" } | { kind:"cons", head:A, tail:List<A> }

type M<A> = List<A>
export let empty = <A>() : List<A> => ({ kind:"empty" })
export let cons  = <A>(x:A, xs:List<A>) : List<A> => ({ kind:"cons", head:x, tail:xs })

export let map = <A,B>(f:(_:A)=>B, x:M<A>) : M<B> =>
  x.kind == "empty" ? empty<B>() : cons<B>(f(x.head), map<A,B>(f, x.tail))

export let filter = <A>(p:(_:A)=>boolean, x:M<A>) : M<A> =>
  x.kind == "empty" ? empty<A>() :
  p(x.head) ? cons<A>(x.head, filter(p, x.tail)) :
  filter(p, x.tail)

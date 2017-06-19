export type Option<A> = { kind:"none" } | { kind:"some", value:A }

type M<A> = Option<A>
export let none = <A>() : Option<A> => ({ kind:"none" })
export let some = <A>(x:A) : Option<A> => ({ kind:"some", value:x })

export let map = <A,B>(f:(_:A)=>B, x:M<A>) : M<B> =>
  x.kind == "none" ? none<B>() : some<B>(f(x.value))

export let filter = <A>(p:(_:A)=>boolean, x:M<A>) : M<A> =>
  x.kind == "none" || p(x.value) == false ? none<A>() : some<A>(x.value)

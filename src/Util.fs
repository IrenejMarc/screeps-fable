module Util

open Fable.Core
open Fable.Core.JsInterop

let jsObjectKeys = JS.Constructors.Object.keys
let jsObjectValues obj = jsObjectKeys obj |> Seq.map (function key -> obj?(key))
let jsObjectSize obj = jsObjectKeys obj |> Seq.length

let mapProps fn obj =
  obj
  |> jsObjectKeys
  |> Seq.map (fun name -> fn obj?(name))

let repeat (count: int) (fn : (int) -> unit) =
  {0 .. count - 1}
  |> Seq.iter (fun x -> fn x)

let randomInt min max =
  min + JS.Math.random () * (max - min)
  |> JS.Math.trunc

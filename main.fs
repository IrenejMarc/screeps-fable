open Fable.Core

[<Emit("module.exports[$0] = $1")>]
let exportModule (name: string) (value: obj) = jsNative

exportModule "loop" (fun _ ->
  CreepManager.run ScreepsLib.globalGame
)

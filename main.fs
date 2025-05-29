open Fable.Core

[<Emit("module.exports[$0] = $1")>]
let exportModule (name: string) (value: obj) = jsNative

[<Global("Game")>]
let game: ScreepsLib.Game = jsNative

[<Global("Memory")>]
let memory: ScreepsLib.Memory = jsNative

[<Global("console")>]
let console: JS.Console = jsNative

exportModule "loop" (fun _ ->
  CreepManager.run game
)

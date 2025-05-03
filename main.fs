module Main
open ScreepsLib
open Fable.Core
open Fable.Core.JsInterop

let tap x fn =
  fn x
  x

let repeat (count: int) (fn : (int) -> unit) =
  {0 .. count}
  |> Seq.iter (fun x -> fn x)

let JSObject = JS.Constructors.Object

let CREEPS_PER_SPAWN = 2
let WANTED_CREEP_BODY = [| WORK; MOVE; CARRY |];

let spawnMinersForRoom (room: Room): unit =
  let minerCount =
    Game.creeps
    |> JSObject.keys
    |> Seq.length

  let missingMinerCount = CREEPS_PER_SPAWN - minerCount

  if missingMinerCount > 0 then
    repeat missingMinerCount $ fun n ->
      console.log $"Spawning missing creep #{n}"

let spawnMiners () =
  JSObject.keys Game.rooms
  |> Seq.iter spawnMinersForRoom

let runCreeps () = "successfully ran?"
let cleanup () = "clean"

let run () = spawnMiners ()

exportModule "loop" run

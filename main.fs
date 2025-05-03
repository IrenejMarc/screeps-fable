module Main
open System
open ScreepsLib
open Fable.Core
open Fable.Core.JsInterop

let random = new Random()

let tap x fn =
  fn x
  x

let repeat (count: int) (fn : (int) -> unit) =
  {0 .. count - 1}
  |> Seq.iter (fun x -> fn x)

let JSObject = JS.Constructors.Object
let jsObjectKeys = JSObject.keys
let jsObjectSize = jsObjectKeys >> Seq.length

let mySpawns = 
  jsObjectKeys Game.spawns
  |> Seq.map (fun name -> Game.spawns?(name) :> Spawn)

let CREEPS_PER_ROOM = 2
let WANTED_CREEP_BODY = [| WORK; MOVE; CARRY |];

let spawnMinersForSpawn (spawn: Spawn): unit =
  let minerCount = jsObjectSize Game.creeps

  let missingMinerCount = CREEPS_PER_ROOM - minerCount

  if missingMinerCount > 0 then
    repeat missingMinerCount $ fun _ ->
      let creepName = $"C {random.Next(100, 900)}"
      let creepSpawnResult =
        spawn.spawnCreep WANTED_CREEP_BODY creepName

      console.log $"Spawning missing creep '{creepName}' for {spawn.name}- result: {creepSpawnResult}"

let spawnMiners () =
  mySpawns |> Seq.iter spawnMinersForSpawn

let run () = do
  spawnMiners ()

exportModule "loop" run

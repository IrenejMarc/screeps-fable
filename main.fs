module Main
open System
open ScreepsLib
open Fable.Core
open Fable.Core.JsInterop

let randomInt min max =
  min + JS.Math.random () * (max - min)
  |> JS.Math.trunc

let repeat (count: int) (fn : (int) -> unit) =
  {0 .. count - 1}
  |> Seq.iter (fun x -> fn x)

let jsObjectKeys = JS.Constructors.Object.keys
let jsObjectSize = jsObjectKeys >> Seq.length

let mySpawns () = 
  jsObjectKeys Game.spawns
  |> Seq.map (fun name -> Game.spawns?(name) :> Spawn)

let CREEPS_PER_ROOM = 2
let WANTED_CREEP_BODY = [| WORK; MOVE; CARRY |];

let spawnMinersForSpawn (spawn: Spawn): unit =
  let minerCount = jsObjectSize Game.creeps
  let missingMinerCount = CREEPS_PER_ROOM - minerCount

  if missingMinerCount > 0 then
    repeat missingMinerCount $ fun _ ->
      let creepName = $"C {randomInt 100 900}"
      let creepSpawnResult =
        spawn.spawnCreep WANTED_CREEP_BODY creepName (createObj [ "role", "builder" ])

      console.log $"Spawning missing creep '{creepName}' for {spawn.name}- result: {creepSpawnResult}"

let spawnMiners () =
  mySpawns() |> Seq.iter spawnMinersForSpawn

[<StringEnum>]
type CreepRole =
  | Miner
  | Builder
  | Upgrader

[<AllowNullLiteral>]
type MyCreepMemory =
  abstract role: CreepRole option with get, set

type MyCreep =
  inherit Creep
  abstract memory: MyCreepMemory with get, set

[<StringEnum>]
type CreepRunResult =
  | Working
  | Waiting
  | TaskCompleted
  | NotImplemented

let myCreeps () = 
  jsObjectKeys Game.creeps
  |> Seq.map (fun name -> Game.creeps?(name) :> MyCreep)

let runBuilder (creep: MyCreep): CreepRunResult =
  console.log $"Creep {creep.name} is buldeirng"
  Working

let runMiner (creep: MyCreep) =
  console.log $"Creep {creep.name} is working"
  creep.room.find(FIND_SOURCES)
  Working

let runCreep (creep: MyCreep) =
  match creep.memory.role with
  | Some Builder  -> creep, runBuilder creep
  | Some Miner    -> creep, runMiner creep
  | Some Upgrader -> creep, NotImplemented
  | None          -> creep, Waiting

let handleRunResult ((creep, result): MyCreep * CreepRunResult) =
  match result with
  | Working -> ()
  | Waiting -> console.log $"No task for {creep.name} :(" // TODO: assign new task!
  | TaskCompleted -> () // TODO: ditto
  | NotImplemented -> console.log "Task not implemented" // TODO: ditto

let runCreeps () =
  myCreeps()
  |> Seq.map runCreep
  |> Seq.iter handleRunResult

let run () = do
  spawnMiners()
  runCreeps()

exportModule "loop" run

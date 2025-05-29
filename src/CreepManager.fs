module CreepManager
open ScreepsLib.GlobalConstants
open Fable.Core.JsInterop

let CREEPS_PER_ROOM = 2
let WANTED_CREEP_BODY = [| WORK; MOVE; CARRY |];

let spawnMinersForSpawn game (spawn: ScreepsLib.Spawn): unit =
  let minerCount = Util.jsObjectSize <| Game.creeps game
  let missingMinerCount = CREEPS_PER_ROOM - minerCount

  if missingMinerCount > 0 then
    Util.repeat missingMinerCount <| fun _ ->
      let creepName = $"C {Util.randomInt 100 900}"
      let creepSpawnResult =
        spawn |> Spawn.spawnCreep WANTED_CREEP_BODY creepName (createObj [ "role", "builder" ])

      Log.info $"Spawning missing creep '{creepName}' for {spawn.name}- result: {creepSpawnResult}"

let spawnMiners game =
  Game.spawns game
  |> Seq.iter (spawnMinersForSpawn game)



let runBuilder (creep: ScreepsLib.Creep): CreepRunResult =
  Log.info $"Creep {creep.name} is buldeirng"
  Working


let runCreep (creep: ScreepsLib.Creep) =
  let memory = Creep.memory creep

  match memory.role with
  | Some Builder  -> creep, runBuilder creep
  | Some Miner    -> creep, Miner.run creep
  | Some Upgrader -> creep, NotImplemented
  | None          -> creep, Waiting


let assignCreepTask creep =
  let task = Miner

  Log.info $"Assigning {task} to {creep?name}"

  creep
  |> Creep.assignTask task


let handleRunResult ((creep, result): ScreepsLib.Creep * CreepRunResult) =
  match result with
  | Working -> ()
  | Waiting -> assignCreepTask creep
  | TaskCompleted -> () // TODO: ditto
  | NotImplemented -> Log.info "Task not implemented" // TODO: ditto
  | UnhandledError message -> Log.info $"Unhandled error: {message}"

let runCreeps game =
  Game.creeps game
  |> Seq.map runCreep
  |> Seq.iter handleRunResult

let run (game: ScreepsLib.Game) =
  spawnMiners game
  runCreeps game

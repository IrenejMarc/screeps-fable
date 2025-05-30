module CreepManager
open ScreepsLib.GlobalConstants

let handleRunResult ((creep, result): ScreepsLib.Creep option * CreepRunResult) =
  match (creep, result) with
  | None, _          -> Log.info $"Creep not found"
  | _, Working       -> Log.info "Creep working"
  | _, Spawning      -> Log.info "Waiting for creep to spawn"

  | _, NotImplemented -> Log.info "Task not implemented"
  | _, UnhandledError message -> Log.info $"Unhandled error: {message}"


let bodyCost parts = Seq.length parts * 100
let spawnMiner name parts spawn =
  let cost = bodyCost parts
  let availableEnergy = Store.usedCapacity RESOURCE_ENERGY spawn

  if cost <= availableEnergy then
    let spawnResult = Spawn.spawnCreep parts name spawn

    match spawnResult with
    | OK -> Spawning
    | e -> UnhandledError $"Miner spawn error {e}"
  else
    UnhandledError $"Not enough energy to spawn {name} ({availableEnergy}/{cost})"

let makeMinerBody() =
  [| WORK; MOVE; CARRY |];

let runMiner (source, name)  =
  match Game.creep name with
  | Some creep -> Some creep, Miner.run creep source
  | None       -> None, spawnMiner name (makeMinerBody()) (Game.spawn "Spawn1")

let MINERS_PER_SOURCE = 2

let runMiners game =
  let sourceMinerNames (source: ScreepsLib.Source) =
    {1 .. MINERS_PER_SOURCE}
    |> Seq.map (fun n -> source, $"M{n}{source.id}")

  Game.rooms game
  |> Seq.collect (Room.find FIND_SOURCES)
  |> Seq.collect sourceMinerNames
  |> Seq.iter (runMiner >> handleRunResult)

let run (game: ScreepsLib.Game) =
  runMiners game

module Miner
open ScreepsLib.GlobalConstants
open Screeps

module Actions =
  let actionError action e = UnhandledError $"[Creep Action][{action}] Error {e}"

  let move target creep =
    creep 
    |> Creep.moveTo target
    |> function
      | OK        -> Working
      | ERR_TIRED -> Working
      | e -> actionError "move" e

  let harvest target creep =
    creep
    |> Creep.harvest target
    |> function
      | OK -> Working
      | ERR_NOT_IN_RANGE -> move target creep
      | e -> actionError "harvest" e

  let transfer target creep =
    creep
    |> Creep.transfer target RESOURCE_ENERGY
    |> function
      | OK -> Working
      | ERR_NOT_IN_RANGE -> move target creep
      | e -> actionError "transfer" e

[<Fable.Core.StringEnum>]
type Job =
| Mine
| Transfer

type MinerMemory =
  inherit CreepMemory

  abstract targetStoreId:  ID option with get, set

  abstract job: Job option with get, set

type MinerCreep =
  inherit ScreepsLib.Creep

  abstract memory: MinerMemory

let findStructures structure room = 
  Room.find<ScreepsLib.Structure> FIND_STRUCTURES room
  |> Seq.filter (fun (s) -> s.structureType = structure)

let getStore targetStoreId miner =
  let target = Game.get targetStoreId
  let room = Creep.room miner

  match target with
  | Some t -> t
  | None   ->
    findStructures STRUCTURE_SPAWN room
    |> Seq.filter (fun s -> ((Store.freeCapacity RESOURCE_ENERGY s) > 0))
    |> Seq.append (findStructures STRUCTURE_CONTROLLER room)
    |> Seq.tryHead

let run (creep: ScreepsLib.Creep) target =
  let miner = creep :?> MinerCreep
  let store = (getStore miner.memory.targetStoreId miner)
  let freeCapacity = Store.freeCapacity RESOURCE_ENERGY creep
  let usedCapacity = Store.usedCapacity RESOURCE_ENERGY creep

  match miner.memory.job, freeCapacity with
  | Some Mine, 0 -> 
    miner.memory.job <- Some Transfer
    Actions.transfer store miner
  | Some Transfer, 0 -> 
    miner.memory.job <- Some Mine
    Actions.harvest store miner
  | Some Mine, _ when freeCapacity > 0 -> Actions.harvest target miner
  | Some Transfer, _ when usedCapacity > 0 -> Actions.transfer store miner
  | _, _ ->
      miner.memory.job <- Some Mine
      Actions.harvest store miner

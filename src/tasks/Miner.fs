module Miner
open ScreepsLib.GlobalConstants
open Screeps

let isFull creep =
  let free = creep |> Creep.freeCapacity RESOURCE_ENERGY

  free <= 0

let move target creep =
  creep |> Creep.moveTo target
  |> function
    | OK -> Working
    | e -> UnhandledError $"Error moving {e}"


let findTarget (creep: ScreepsLib.Creep) =
  creep.room
  |> Room.find FIND_SOURCES
  |> Seq.head

let harvest creep =
  let target = findTarget creep

  let harvestResult =
    creep
    |> Creep.harvest target

  match harvestResult with
  | OK -> Working
  | ERR_NOT_IN_RANGE -> move target creep
  | _ -> UnhandledError $"Harvest error {harvestResult}"

let upgrade (creep: ScreepsLib.Creep) =
  let target =
    creep.room
    |> Room.find FIND_STRUCTURES
    |> Seq.head

  let dropoffResult =
    creep
    |> Creep.transfer target RESOURCE_ENERGY

  match dropoffResult with
  | OK -> Working
  | ERR_NOT_IN_RANGE -> move target creep
  | _ -> UnhandledError $"Harvest error {dropoffResult}"


let run (creep: ScreepsLib.Creep) =
  if isFull creep then
    upgrade creep
  else
    harvest creep



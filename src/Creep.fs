[<AutoOpen>]
module Creep

open Fable.Core
open Fable.Core.JsInterop
open ScreepsLib.GlobalConstants

[<StringEnum>]
type CreepRole =
  | Miner
  | Builder
  | Upgrader

type CreepMemory =
  abstract role: CreepRole option with get, set


type CreepRunResult =
  | Spawning
  | Working
  | NotImplemented
  | UnhandledError of string

module Creep =
  let room creep: ScreepsLib.Room = creep?room

  let memory creep: CreepMemory = creep?memory
  let harvest target creep: CreepActionResult = creep?harvest(target)
  let transfer target resource creep: CreepActionResult = creep?transfer(target, resource)

  let assignTask (task: CreepRole) (creep: ScreepsLib.Creep) =
    creep.memory?role <- task

  let moveTo target creep: CreepActionResult = creep?moveTo(target)

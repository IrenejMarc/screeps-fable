[<AutoOpen>]
module Screeps

open Fable.Core
open Fable.Core.JsInterop
open ScreepsLib.GlobalConstants

module Log =
  let [<Emit("console")>] console: JS.Console = jsNative

  let info value  = console.log value
  let error value = console.error value
  let debug value = console.debug value

module Spawn =
  let spawnCreep body name options spawn: float =
    spawn?spawnCreep (body, name, options)

module Game =
  let spawns game: ScreepsLib.Spawn seq = Util.jsObjectValues game?spawns
  let creeps game: ScreepsLib.Creep seq = Util.jsObjectValues game?creeps

module Creep =
  let memory creep: CreepMemory = creep?memory
  let harvest target creep: CreepActionResult = creep?harvest(target)
  let transfer target resource creep: CreepActionResult = creep?transfer(target, resource)
  let freeCapacity resource creep: float = creep?store?getFreeCapacity(resource)

  let assignTask (task: CreepRole) (creep: ScreepsLib.Creep) =
    creep?memory?role <- task

  let moveTo target creep: CreepActionResult = creep?moveTo(target)

module Room =
  let find it room = room?find(it)

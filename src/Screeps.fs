[<AutoOpen>]
module Screeps

open Fable.Core
open Fable.Core.JsInterop
open ScreepsLib

type ID = string

module Log =
  let [<Emit("console")>] console: JS.Console = jsNative

  let info value  = console.log value
  let error value = console.error value
  let debug value = console.debug value

  let tap fn value = fn value; value



module Spawn =
  let spawnCreep body name spawn: float = spawn?spawnCreep(body, name)

module Game =
  let get id = ScreepsLib.globalGame?getObjectById(id)
  let creep (name: string): Creep option = globalGame.creeps?(name)
  let spawn (name: string): Spawn option = globalGame.spawns?(name)

  let spawns game: Spawn seq = Util.jsObjectValues game?spawns
  let creeps game: Creep seq = Util.jsObjectValues game?creeps
  let rooms game: Room seq = Util.jsObjectValues game?rooms

module Room =
  let find<'t> it room: 't seq = room?find(it)

module Store =
  let freeCapacity resource object: int =
    object?store?getFreeCapacity(resource)

  let usedCapacity resource object: int =
    object?store?getUsedCapacity(resource)



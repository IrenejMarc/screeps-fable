module ScreepsLib
open Fable.Core

type [<AllowNullLiteral>] Spawn =
  abstract name: string with get, set

type [<AllowNullLiteral>] Room =
  abstract member name: string

type [<AllowNullLiteral>] Creep =
  abstract name:   string    with get, set
  abstract memory: obj with get, set 
  abstract room: Room with get, set

type [<AllowNullLiteral>] Memory =
  abstract flags:  JS.Object with get, set
  abstract rooms:  JS.Object with get, set
  abstract spawns: JS.Object with get, set
  abstract creeps: JS.Object with get, set

type [<AllowNullLiteral>] Game =
  abstract spawns: JS.Object with get, set
  abstract creeps: JS.Object with get, set

  abstract flags:             JS.Object with get, set
  abstract rooms:             JS.Object with get, set
  abstract structures:        JS.Object with get, set
  abstract constructionSites: JS.Object with get, set
  abstract time:              float     with get, set

  abstract getObjectById: id: string -> 'T

module GlobalConstants =
  let [<Global>] WORK:  string = jsNative
  let [<Global>] MOVE:  string = jsNative
  let [<Global>] CARRY: string = jsNative

  let [<Global>] RESOURCE_ENERGY: string = jsNative

  let [<Global>] FIND_SOURCES: float = jsNative
  let [<Global>] FIND_STRUCTURES: float = jsNative

  let [<Global>] STRUCTURE_SPAWN: float = jsNative

  type CreepActionResult = float
  let [<Literal>][<Global>] OK: CreepActionResult = 0.0
  let [<Literal>][<Global>] ERR_NOT_OWNER: CreepActionResult = -1.0
  let [<Literal>][<Global>] ERR_NOT_RANGE: CreepActionResult = -2.0
  let [<Literal>][<Global>] ERR_NOT_IN_RANGE: CreepActionResult = -9.0

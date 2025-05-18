module ScreepsLib
open Fable.Core

[<Emit("module.exports[$0] = $1")>]
let exportModule (name: string) (value: obj) = jsNative

[<Global("console")>]
let console: JS.Console = jsNative

[<Global("Object")>]
let JSObject: JS.Object = jsNative

[<Global>]
let FIND_SOURCES: float = jsNative

type [<AllowNullLiteral>] Spawn =
  abstract name: string with get, set
  abstract member spawnCreep: string array -> string -> obj -> float

type [<AllowNullLiteral>] Room =
  abstract member name: string
  abstract member find: float -> options: obj -> ResizeArray<'a>

type [<AllowNullLiteral>] Creep =
  abstract name:   string    with get, set
  abstract memory: JS.Object with get, set 
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

let [<Global>] WORK:  string = jsNative
let [<Global>] MOVE:  string = jsNative
let [<Global>] CARRY: string = jsNative

let [<Global>] Memory: Memory = jsNative
let [<Global>] Game: Game = jsNative

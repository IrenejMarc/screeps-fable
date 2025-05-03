module ScreepsLib
open Fable.Core

[<Emit("module.exports[$0] = $1")>]
let exportModule (name: string) (value: obj) = jsNative

[<Global("console")>]
let console: JS.Console = jsNative

[<Global("Object")>]
let JSObject: JS.Object = jsNative

type [<AllowNullLiteral>] JSMapObject<'V> =
  [<Emit("$0[$1]")>]
  member self.get a: 'V = jsNative

type [<AllowNullLiteral>] Spawn =
  abstract name: string with get, set
  abstract member spawnCreep: string array -> string -> JS.Object

type [<AllowNullLiteral>] Creep =
  abstract name: string with get, set

type [<AllowNullLiteral>] Memory =
  [<Emit("$0[$1]{{=$2}}")>] abstract Item: name: string -> JS.Object with get, set
  abstract flags:  JS.Object with get, set
  abstract rooms:  JS.Object with get, set
  abstract spawns: JS.Object with get, set
  abstract creeps: JS.Object with get, set

type [<AllowNullLiteral>] Game =
  abstract spawns: JS.Object with get, set
  abstract creeps: JS.Object with get, set

  abstract flags:             JS.Object   with get, set
  abstract rooms:             JS.Object   with get, set
  abstract structures:        JS.Object   with get, set
  abstract constructionSites: JS.Object   with get, set
  abstract time:              float with get, set

  abstract getObjectById: id: string -> 'T

let [<Global>] WORK:  string = jsNative
let [<Global>] MOVE:  string = jsNative
let [<Global>] CARRY: string = jsNative

type FindT = JS.Object

type [<AllowNullLiteral>] Room =
  abstract member name: string
  abstract member find: FindT -> JS.Object


let [<Global>] Memory: Memory = jsNative
let [<Global>] Game: Game = jsNative

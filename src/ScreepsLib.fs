module ScreepsLib
open Fable.Core

[<Emit("module.exports[$0] = $1")>]
let exportModule (name: string) (value: obj) = jsNative

[<Global("console")>]
let console: JS.Console = jsNative

[<Global("Object")>]
let JSObject: JS.Object = jsNative

type [<AllowNullLiteral>] Spawn =
  abstract name: string with get, set

type [<AllowNullLiteral>] Creep =
  abstract name: string with get, set

type [<AllowNullLiteral>] Memory =
  [<Emit("$0[$1]{{=$2}}")>] abstract Item: name: string -> JS.Object with get, set
  abstract flags:  JS.Object with get, set
  abstract rooms:  JS.Object with get, set
  abstract spawns: JS.Object with get, set
  abstract creeps: JS.Object with get, set

type [<AllowNullLiteral>] Game =
  abstract creeps:            Map<string, Creep>   with get, set
  abstract flags:             JS.Object   with get, set
  abstract rooms:             JS.Object   with get, set
  abstract structures:        JS.Object   with get, set
  abstract constructionSites: JS.Object   with get, set
  abstract time:              float with get, set
  abstract spawns: Map<string, Spawn> with get, set

  abstract getObjectById: id: string -> 'T

let WORK = "work"
let MOVE = "move"
let CARRY = "carry"

type FindT = JS.Object

type [<AllowNullLiteral>] Room =
  member __.name with get(): string = failwith "JSOnly"
  member __.find (findType: FindT): JS.Object = failwith "JSOnly"


let [<Global>] Memory: Memory = jsNative
let [<Global>] Game: Game = jsNative

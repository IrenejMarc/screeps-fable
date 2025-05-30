module ScreepsLib
open Fable.Core

type [<AllowNullLiteral>] Store =
  abstract getFreeCapacity: string -> float
  abstract getUsedCapacity: string -> float
  abstract getCapacity:     string -> float

type [<AllowNullLiteral>] Spawn =
  abstract name: string with get, set
  abstract store: Store     with get, set

type [<AllowNullLiteral>] Room =
  abstract member name: string

type [<AllowNullLiteral>] Creep =
  abstract name:   string    with get, set
  abstract memory: obj       with get, set 
  abstract room:   Room      with get, set
  abstract store:  Store     with get, set

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

type [<AllowNullLiteral>] Structure =
  abstract id:            string with get
  abstract structureType: string with get

type [<AllowNullLiteral>] Source =
  abstract id: string with get

  abstract energy:              float with get
  abstract energyCapacity:      float with get
  abstract ticksToRegeneration: float with get

  abstract getObjectById: id: string -> 'T

let [<Global("Game")>] globalGame: Game = jsNative

module GlobalConstants =
  let [<Global>] WORK:  string = jsNative
  let [<Global>] MOVE:  string = jsNative
  let [<Global>] CARRY: string = jsNative

  let [<Global>] RESOURCE_ENERGY: string = jsNative

  let [<Global>] FIND_SOURCES: float = jsNative
  let [<Global>] FIND_STRUCTURES: float = jsNative


  type CreepActionResult = float
  let [<Literal>][<Global>] OK:                        CreepActionResult = 0.0
  let [<Literal>][<Global>] ERR_NOT_OWNER:             CreepActionResult = -1.0
  let [<Literal>][<Global>] ERR_NO_PATH:               CreepActionResult = -2.0
  let [<Literal>][<Global>] ERR_NAME_EXISTS:           CreepActionResult = -3.0
  let [<Literal>][<Global>] ERR_BUSY:                  CreepActionResult = -4.0
  let [<Literal>][<Global>] ERR_NOT_FOUND:             CreepActionResult = -5.0
  let [<Literal>][<Global>] ERR_NOT_ENOUGH_ENERGY:     CreepActionResult = -6.0
  let [<Literal>][<Global>] ERR_NOT_ENOUGH_RESOURCES:  CreepActionResult = -6.0
  let [<Literal>][<Global>] ERR_INVALID_TARGET:        CreepActionResult = -7.0
  let [<Literal>][<Global>] ERR_FULL:                  CreepActionResult = -8.0
  let [<Literal>][<Global>] ERR_NOT_IN_RANGE:          CreepActionResult = -9.0
  let [<Literal>][<Global>] ERR_INVALID_ARGS:          CreepActionResult = -10.0
  let [<Literal>][<Global>] ERR_TIRED:                 CreepActionResult = -11.0
  let [<Literal>][<Global>] ERR_NO_BODYPART:           CreepActionResult = -12.0
  let [<Literal>][<Global>] ERR_NOT_ENOUGH_EXTENSIONS: CreepActionResult = -6.0
  let [<Literal>][<Global>] ERR_RCL_NOT_ENOUGH:        CreepActionResult = -14.0
  let [<Literal>][<Global>] ERR_GCL_NOT_ENOUGH:        CreepActionResult = -15.0

  let [<Literal>][<Global>] STRUCTURE_SPAWN = "spawn"
  let [<Literal>][<Global>] STRUCTURE_EXTENSION = "extension"
  let [<Literal>][<Global>] STRUCTURE_ROAD = "road"
  let [<Literal>][<Global>] STRUCTURE_WALL = "constructedWall"
  let [<Literal>][<Global>] STRUCTURE_RAMPART = "rampart"
  let [<Literal>][<Global>] STRUCTURE_KEEPER_LAIR = "keeperLair"
  let [<Literal>][<Global>] STRUCTURE_PORTAL = "portal"
  let [<Literal>][<Global>] STRUCTURE_CONTROLLER = "controller"
  let [<Literal>][<Global>] STRUCTURE_LINK = "link"
  let [<Literal>][<Global>] STRUCTURE_STORAGE = "storage"
  let [<Literal>][<Global>] STRUCTURE_TOWER = "tower"
  let [<Literal>][<Global>] STRUCTURE_OBSERVER = "observer"
  let [<Literal>][<Global>] STRUCTURE_POWER_BANK = "powerBank"
  let [<Literal>][<Global>] STRUCTURE_POWER_SPAWN = "powerSpawn"
  let [<Literal>][<Global>] STRUCTURE_EXTRACTOR = "extractor"
  let [<Literal>][<Global>] STRUCTURE_LAB = "lab"
  let [<Literal>][<Global>] STRUCTURE_TERMINAL = "terminal"
  let [<Literal>][<Global>] STRUCTURE_CONTAINER = "container"
  let [<Literal>][<Global>] STRUCTURE_NUKER = "nuker"
  let [<Literal>][<Global>] STRUCTURE_FACTORY = "factory"
  let [<Literal>][<Global>] STRUCTURE_INVADER_CORE = "invaderCore"

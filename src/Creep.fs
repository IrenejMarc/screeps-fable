[<AutoOpen>]
module Creep

open Fable.Core

[<StringEnum>]
type CreepRole =
  | Miner
  | Builder
  | Upgrader

type CreepMemory =
  abstract role: CreepRole option with get, set

type CreepRunResult =
  | Working
  | Waiting
  | TaskCompleted
  | NotImplemented
  | UnhandledError of string

module FAKE.WindowsHostHelper  

type HostName = |Host of string

val readHostsFile: unit -> string[]

val withoutHostEntry: HostName -> string[] -> string[]
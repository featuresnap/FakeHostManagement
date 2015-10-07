// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System.Text.RegularExpressions

let (|ParseRegex|_|) regex str = 
    let m = Regex(regex).Match str
    if m.Success 
    then Some (List.tail [ for x in m.Groups -> x.Value ])
    else None

let containsHost host = 
    match host with 
    |ParseRegex "([a-z|A-Z|0-9|-])" [hostName] -> true
    |_ -> false


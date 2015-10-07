module FAKE.WindowsHostHelper 

    open System.IO
    open System
    open System.Text
    open System.Text.RegularExpressions

    type HostName = |Host of string

    let doesNotContain (Host(testArg))  = function 
        |(value:string) -> 
            Regex.IsMatch(value, sprintf "\\b%s\\b" testArg)
            |> not

    let private (</>) a b = Path.Combine(a,b)

    let private getHostsFilePath () = 
        Environment.GetFolderPath(Environment.SpecialFolder.System) </> "drivers" </> "etc" </> "hosts"
    
    let private reverseParams f = fun b a -> f a b

    let withoutHostEntry host lines = 
        lines |> Array.filter (doesNotContain host)
    
    let withHostEntry (Host host) lines = 
        let addLine line lines = Array.append lines [|line|] 

        lines
        |> withoutHostEntry (Host host)
        |> addLine (sprintf "127.0.0.1 %s" host)

    let withHostEntries hosts lines = 
        let addHostLine = withHostEntry |> reverseParams 
        hosts |> Seq.fold addHostLine lines 

    let readHostsFile () = File.ReadAllLines(getHostsFilePath(), Encoding.ASCII)

    let writeHostsFile lines = File.WriteAllLines(getHostsFilePath(), lines, Encoding.ASCII)

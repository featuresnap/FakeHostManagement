module TestWindowsHostHelper

open Xunit
open FsCheck
open FAKE.WindowsHostHelper

let hostLines = 
  [|"127.0.0.1 foo.com";
    "127.0.0.1 bar.com";
    "127.0.0.1 baz.com";|]

[<Fact>]
let ``Can read windows hosts file`` () = 
    readHostsFile() |> Assert.NotEmpty

[<Fact>] 
let ``Removing a host entry`` () = 
    let modified = hostLines |> withoutHostEntry (Host "baz.com")
    Assert.Equal<string seq>(hostLines |> Array.take 2, modified)


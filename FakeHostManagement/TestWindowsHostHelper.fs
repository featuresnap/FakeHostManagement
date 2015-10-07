module TestWindowsHostHelper

open Xunit
open FsCheck
open FAKE.WindowsHostHelper

let hostLines = 
  [|"127.0.0.1 foo.com";
    "127.0.0.1 bar.com";
    "127.0.0.1 baz.com";|]

[<Fact>] 
let ``Removing a non-existent host entry leaves all lines`` () = 
    let modified = hostLines |> withoutHostEntry (Host "quux.com")
    Assert.Equal<string seq>(hostLines, modified)

[<Fact>] 
let ``Removing an existing host entry leaves the remaining lines`` () = 
    let modified = hostLines |> withoutHostEntry (Host "foo.com")
    Assert.Equal<string seq>(hostLines |> Array.skip 1, modified)

[<Fact>] 
let ``Partial host names do not qualify to be removed``() =
    let modified = hostLines |> withoutHostEntry (Host "com")
    Assert.Equal<string seq>(hostLines, modified)

[<Fact>]
let ``Can read windows hosts file`` () = 
    readHostsFile() |> Assert.NotEmpty


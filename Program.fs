// For more information see https://aka.ms/fsharp-console-apps

(*
CSV Validator program

Read in the file using the CsvFile library.

module: Define validation scheme.
Read in the validation parameters from Json file.
I need to be able to dynamically use a validator with a specific field type.
At the core of this is a filter operation operating in a pipeline.
I will first make this work (and learn some F# in the process) and then make it fast.
At the core will be a validation pipeline, with each validity check for a row in a 
different section of the pipe.
To make this work, each row and its errors will be repesented as a record.
Use update on records to update the error list for each row. Also the line number.

*)

open System.IO
open FSharp.Data
open FSharp.Json
open Newtonsoft.Json.Linq


[<Literal>]
//let carsparams = @"C:\data\carsparams.json"
//let inFile     = @"C:\data\cars.csv"

let separator = "|"
type Cars = JsonProvider<JsonSample.jsonSample>

//let paramsRecord paramsString =
//    //type Cars = JsonProvider<paramsString>
//    let jsonParams = Cars.Load carsparams
//    printf "%A" jsonParams.JsonValue

type MyJson = JsonProvider<"""{
    "arr": [
        {
            "node": "xyz",
            "type": "string"
        },
        {
            "node": {
                "moredata": "values",
                "otherdata": "values2"
            },
            "type": "node"
        }
    ]
}""">

//--------------------------------------------------------

// Your record type
type RecordType = {
    stringMember: string
    intMember: int
}

let data: RecordType = { stringMember = "The string"; intMember = 123 }

// serialize record into JSON
let json = Json.serialize data
printfn "%s" json
// json is """{ "stringMember": "The string", "intMember": 123 }"""

// deserialize from JSON to record
let deserialized = Json.deserialize<RecordType> json
printfn "%A" deserialized
// deserialized is {stringMember = "some value"; intMember = 123;}

//------------------------------------------------------------------------------

type ValidationParams = {
    name    : string
    desc    : string option
    category: string option
    validValues: string list option
    minLen  : int option
    maxLen  : int option
}

type ParamsRecord = {
    csvFilePath: string
    delimiter  : string
    maxBadRows : int
    numHeaderLines: int
    validationList: ValidationParams list
}

let deserialized2 = Json.deserialize<ParamsRecord> JsonSample.jsonSample
printfn "deserialized2 = %A" deserialized2

//-------------------------------------------------------------

let json1 = MyJson.GetSample()

let a = json1.Arr
let node = a
let nodeStringArray1 =
    node
    |> Array.map (fun x -> x.Node.String)
    |> Array.choose id
printfn "%s" nodeStringArray1[0]  //node.[0].Node

let nodeStrArray =
    a 
    |> Array.map (fun x -> x.Node.String) 
    |> Array.choose id

printfn "%s" nodeStrArray[0]


[<EntryPointAttribute>]
let main args =
    let paramString = File.ReadAllText args.[0]
    //printfn "%A" paramString
    //printfn "%A" paramsRecord
    // The following just generates a tree of seq [] objects. Not sure what's wrong.
    //let obj = JObject.Parse paramString
    //printfn "%A" obj
    //let delim = obj["MaxBadRows"]
    //printfn "%A" delim
    0
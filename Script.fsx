//#r "FSharp.Data.dll"
open FSharp.Data
open Params

//printfn "%i" jsonParams.MaxBadRows

let csv = CsvFile.Load(inFile, hasHeaders = true)

let readlines filepath = 
    System.IO.File.ReadLines filepath
    |> Seq.toList

let lines = readlines inFile

//let printLines =
//    for line in lines do
//        printfn "%s" line

let splitLine (line: string) =
    line.Split separator
    |> Seq.toList

let getFieldsFromLines lines =
    lines
    |> List.map splitLine
    
let fields allLines =
    getFieldsFromLines allLines |> Seq.toList

let fieldsInFile filename =
    filename
    |> readlines
    |> fields

//let printFields =
//    let fieldLines = fieldsInFile inFile
//    for line in fieldLines do
//        for field in line do
//            printfn "%s" field

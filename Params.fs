/// This module takes as input the path to a JSON config file for the validation
/// and returns a record ccontaining all the configuration parameters, with
/// defaults subsstituted in when parameters are missing from the file.

module Params

open FSharp.Data

[<Literal>]
let carsparams = @"C:\data\carsparams.json"
let inFile     = @"C:\data\cars.csv"

let separator = "|"
type Cars = JsonProvider<carsparams>

let paramsRecord paramsString =
    //type Cars = JsonProvider<paramsString>
    let jsonParams = Cars.Load carsparams
    printf "%A" jsonParams.JsonValue
module JsonSample

open FSharp.Json


[<Literal>]
let jsonSample = """
{
"csvFilePath": "C:/data/cars.csv",
"delimiter": ",",
"maxBadRows": 2,
"numHeaderLines": 0,
"validationList": [
    {
        "name": "Year",
        "desc": "The year the car was made.",
        "category"   : "integer",
        "validValues": ["1996", "1997", "1998", "1999"]
    },
    {
        "name": "Make",
        "validValues": ["Jeep", "Chevy", "Buick"]
    },
    {
        "name": "Model"
    },
    {
        "name": "Description",
        "minLen": 0,
        "maxLen": 35
    }
]
}
"""

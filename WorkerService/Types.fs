namespace Database

module Types = 

    [<CLIMutable>]
    type Person = {
        Id : int32
        Name : string
        Contact : string option
    }


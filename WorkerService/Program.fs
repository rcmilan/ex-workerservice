module workerservice.App

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

open workerservice.NumberService

[<EntryPoint>]
let main argv = 
    printfn "Iniciando Worker!"

    let host = 
        Host.CreateDefaultBuilder(argv)
            .ConfigureServices(fun hostContext services ->
                services.AddHostedService<WorkerSample1>() |> ignore
            ).Build().Run()

    0

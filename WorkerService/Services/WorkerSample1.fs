namespace workerservice

module NumberService = 

    open System.Threading
    open System.Threading.Tasks
    open Microsoft.Extensions.Configuration
    open Microsoft.Extensions.Hosting
    open Microsoft.Extensions.Logging
    
    type WorkerSample1(logger : ILogger<WorkerSample1>, configuration : IConfiguration ) =
        inherit BackgroundService()

        override _.ExecuteAsync(stoppingToken : CancellationToken) =
            task {
                // let printNums = (fun x ->  printf "%A\n" x)
                
                // [ for i in 1 .. 100 do yield printNums i ] |> ignore
                
                let printNums = (fun x ->  logger.Log(LogLevel.Information, "{0}", (int)x)   )

                [1..100] |> List.map printNums |> ignore
            } :> Task
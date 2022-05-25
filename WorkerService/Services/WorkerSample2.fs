namespace workerservice

module QuadraticService = 

    open System
    open System.Threading
    open System.Threading.Tasks
    open Microsoft.Extensions.Configuration
    open Microsoft.Extensions.Hosting
    open Microsoft.Extensions.Logging
    
    type WorkerSample2(logger : ILogger<WorkerSample2>, configuration : IConfiguration ) =
        inherit BackgroundService()
        
        override _.ExecuteAsync(stoppingToken : CancellationToken) =
            task {
                let Delta a b c =  Math.Pow(b,2) - 4.0 * a * c

                let X1 a b delta =  (-b + Math.Sqrt(delta)) / (2.0 * a)
                let X2 a b delta =  (-b - Math.Sqrt(delta)) / (2.0 * a)

                let PrintResults a b d =
                    printf "Valor de X1: %f\n" (X1 a b d) |> ignore
                    printf "Valor de X2: %f\n" (X2 a b d) |> ignore
                    
                let paramA = 1.0
                let paramB = -1.0
                let paramC = -12.0
                
                Delta paramA paramB paramC |> PrintResults paramA paramB 
                
            } :> Task
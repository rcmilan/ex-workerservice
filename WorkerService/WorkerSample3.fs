namespace workerservice

module DatabaseService =

    open System.Threading
    open System.Threading.Tasks
    open Microsoft.Extensions.Configuration
    open Microsoft.Extensions.Hosting
    open Microsoft.Extensions.Logging

    open Database
        
    type WorkerSample3(logger : ILogger<WorkerSample3>, configuration : IConfiguration ) =
        inherit BackgroundService()

        override _.ExecuteAsync(stoppingToken : CancellationToken) =
            task {
            
                printf "CRIANDO TABELAS...\n"
                Queries.Schema.CreateTables |> Async.RunSynchronously |> ignore
                
                printf "INSERINDO REGISTROS...\n"
                Queries.Person.New "Pessoa 1" "Email" |> Async.RunSynchronously |> ignore
                Queries.Person.New "Pessoa 2" "Telefone" |> Async.RunSynchronously |> ignore
                Queries.Person.New "Pessoa 3" None |> Async.RunSynchronously |> ignore
                
                printf "BUSCANDO TODOS OS REGISTROS...\n"
                Queries.Person.GetAll() |> Async.RunSynchronously |> Seq.iter (printf "%A\n")

                0 |> ignore
            } :> Task

namespace Database

open Microsoft.Data.Sqlite
open FSharp.Data.Dapper

module private DbConnection =

    let private buildConnectionString (dataSource:string) =
        sprintf "Data Source = %s;" dataSource

    let OnDisk () = new SqliteConnection (buildConnectionString "./example.db")

module Queries =

    let private connection () = Connection.SqliteConnection (DbConnection.OnDisk())

    let querySeqAsync<'T>    = querySeqAsync<'T> (connection)
    let querySingleAsync<'T> = querySingleOptionAsync<'T> (connection)

    module Schema =
        let CreateTables = querySingleAsync<int> {
            script """
                CREATE TABLE IF NOT EXISTS Person (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name VARCHAR(255) NOT NULL,
                    Contact VARCHAR(255) NULL
                );
            """
        }

    module Person =
        let New name contact = querySingleAsync<int> {
            script """INSERT INTO Person (Name, Contact) VALUES (@Name, @Contact)"""
            parameters (dict ["Name", box name; "Contact", box contact])
        }

        let GetSingleByName name = querySingleAsync<Types.Person> {
            script "SELECT * FROM Person WHERE Name = @Name LIMIT 1"
            parameters (dict ["Name", box name])
        }

        let GetAll() = querySeqAsync<Types.Person> { script "SELECT * FROM Person" }

        let UpdateContactByName name contact = querySingleAsync<int> {
            script "UPDATE Person SET Contact = @Contact WHERE Name = @Name"
            parameters (dict ["Contact", box contact; "Name", box name])
        }

        let DeleteByName name = querySingleAsync<int> {
            script "DELETE FROM Person WHERE Name = @Name"
            parameters (dict ["Name", box name])
        }
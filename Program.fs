module SqlHydraForSqlite.Program

open SqlHydra.Query

let openSqlite =
    async {
        let conn = new Microsoft.Data.Sqlite.SqliteConnection("")
        do! conn.OpenAsync() |> Async.AwaitTask

        use attachDbo = conn.CreateCommand(CommandText = "attach database '' as 'dbo'")
        let _ = attachDbo.ExecuteNonQuery()

        use createSchema = conn.CreateCommand(CommandText = SqliteTestDatabase.schema)
        let _ = createSchema.ExecuteNonQuery()

        return conn
    }

let sharedSqlite db =
    let compiler = SqlKata.Compilers.SqliteCompiler()
    ContextType.Shared(new QueryContext(db, compiler))

let insertThenRetrieveDate =
    async {
        use! db = openSqlite

        let ctx = sharedSqlite db

        let! _ =
            insertAsync ctx {
                into dbo.SqliteDateTest
                entity { date = System.DateOnly(2024, 6, 20) }
            }

        let! results =
            selectAsync ctx {
                for row in dbo.SqliteDateTest do
                    toArray
            }

        printfn $"Got %i{results.Length} results"

        return ()
    }

[<EntryPoint>]
let main _ =
    Async.RunSynchronously
    <| async {
        do! insertThenRetrieveDate
        return 0
    }

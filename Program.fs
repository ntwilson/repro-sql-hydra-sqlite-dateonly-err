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

let insertThenRetrieveDateTime =
    async {
        use! db = openSqlite

        let ctx = sharedSqlite db

        let! _ =
            insertAsync ctx {
                into dbo.SqliteDateTime2Test
                entity { dt = System.DateTime(2024, 6, 20, 8, 0, 0) }
            }

        let! results =
            selectAsync ctx {
                for row in dbo.SqliteDateTime2Test do
                    toArray
            }

        printfn $"Got %O{results[0].dt} from the DateTime2 test"

        return ()
    }

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

        printfn $"Got %O{results[0].date} from the Date test"

        return ()
    }

[<EntryPoint>]
let main _ =
    Async.RunSynchronously
    <| async {
        do! insertThenRetrieveDateTime
        do! insertThenRetrieveDate
        return 0
    }

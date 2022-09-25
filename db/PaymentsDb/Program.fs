open DbUp
open System
open System.Reflection

[<EntryPoint>]
let main(args) = 
    let connectionString = match args.Length with
                                    | 0 -> "Data Source=localhost; Initial Catalog=Payments; Integrated Security=True;"
                                    | _ -> args.[0]
    
    EnsureDatabase.For.SqlDatabase(connectionString) |> ignore

    let upgrader = DeployChanges.To.SqlDatabase(connectionString).WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly()).LogToConsole().Build()

    let result = upgrader.PerformUpgrade()

    match result.Successful with
    | true -> 
                Console.ForegroundColor <- ConsoleColor.Green
                Console.WriteLine("Success!")
                Console.ResetColor()
    | false -> 
                Console.ForegroundColor <- ConsoleColor.Red
                Console.WriteLine(result.Error)
                Console.ResetColor()

    0
                            

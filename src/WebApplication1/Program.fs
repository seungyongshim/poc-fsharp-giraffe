open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open System

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    app.MapGet("/help", Func<_,_>(fun http -> task {
        let result = Results.Ok({|
            helo = "Helo"
        |})
        do! result.ExecuteAsync(http)
    })).WithName("help") |> ignore

    app.RunAsync() |> Async.AwaitTask |> Async.RunSynchronously

    0

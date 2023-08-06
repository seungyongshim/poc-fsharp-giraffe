namespace WebApplication1

open Microsoft.AspNetCore.Http
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

module Program =

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)
        let app = builder.Build()

        app.MapGet("/help",  new RequestDelegate(fun http -> task {
            let result = Results.Ok({|
                helo = "Helo"
            |})
            do! result.ExecuteAsync(http)
        }))

        app.Run()

        0

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Giraffe
open Microsoft.AspNetCore.Http
open System.Diagnostics

[<CLIMutable>]
type Car = {
    Hello : string
}

let webapp =
    choose [
        GET >=> choose [
            route "/" >=> setStatusCode 201 >=> json {|
                hello = "world"
            |}
        ]
        POST >=> choose [
            route "/t" >=> handleContext(fun ctx -> task {
                let! car = ctx.TryBindFormAsync<Car>()
                return! ctx.WriteJsonAsync({|
                    traceId = Activity.Current.Id
                |})
            })
        ]
    ]

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddGiraffe() |> ignore
    let app = builder.Build()


    app.UseGiraffe webapp
    app.Run()

    0 // Exit code


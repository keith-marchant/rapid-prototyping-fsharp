module PayableInvoiceTests

open System
open Xunit
open PayableInvoice
open FsUnit.Xunit

let extractError result =
    match result with
    | Ok _ -> failwith "Expected Error"
    | Error err -> err.Errors 

[<Fact>]
let ``When Amount is negative then validation fails`` () =
    let invalidAmount = -10M
    let payableInvoice = createPayableInvoice (1,"ref",invalidAmount,DateTime.Now, 1, Some "Acct Name", Some "123-456", Some "12345678")
    payableInvoice |> validateAmount |> extractError |> should contain (InvalidAmount invalidAmount)

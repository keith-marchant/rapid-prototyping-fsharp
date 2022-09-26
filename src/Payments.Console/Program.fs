open FSharp.Data.Sql
open RoP
open PayableInvoice
open Type1Writer

let [<Literal>] connectionString = "Data Source=localhost;Initial Catalog=Payments;User Id=Payments;Password=Go0d2Use#"

type sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, connectionString, UseOptionTypes = Common.NullableColumnType.OPTION>

let ctx = sql.GetDataContext()

let invoices = 
    query {
        for i in ctx.Dbo.Invoices do
        join c in ctx.Dbo.Contacts on (i.ContactId = c.ContactId)
        where (i.PaymentStatusId = 2)
        select (i.InvoiceId, i.InvoiceReference, i.Amount, i.DueDate, c.ContactId, c.AccountName, c.AccountBsb, c.AccountNumber)
    } 
    |> Seq.map createPayableInvoice

// Debugging, let's see what we retrieved
invoices |> Seq.iter (fun x -> printfn "Record %i, reference %s" x.InvoiceId x.InvoiceReference) |> ignore

type FailureResult<'TResult> = {
    Result : 'TResult
    Errors : string list
}

let (&&&) v1 v2 =
    let addSuccess r1 r2 = r1 // return first
    let addFailure f1 f2 = { Result = f1.Result; Errors = (List.append f1.Errors f2.Errors) } // Combine errors
    plus addSuccess addFailure v1 v2

let validateAccountName payableInvoice =
    match payableInvoice.AccountName with
    | Some x -> Success payableInvoice
    | None _ -> Failure { Result = payableInvoice; Errors = ["Account name is required."]}

let validateAccountBsb payableInvoice =
    match payableInvoice.AccountBsb with
    | Some x -> Success payableInvoice
    | None _ -> Failure { Result = payableInvoice; Errors = ["BSB is required."]}

let validateAccountNumber payableInvoice =
    match payableInvoice.AccountNumber with
    | Some x -> Success payableInvoice
    | None _ -> Failure { Result = payableInvoice; Errors = ["Account number is required."]}

let validatePayableInvoice =
    validateAccountName
    &&& validateAccountBsb
    &&& validateAccountNumber

let validatedInput = invoices |> Seq.map validatePayableInvoice

let printType1Content validatedRecord = 
    printfn "%s" (writeType1LineContent validatedRecord)

let printValidationFailure failure =
    printfn "Invoice Id: %d, Error: %A" failure.Result.InvoiceId failure.Errors

validatedInput |> Seq.iter (fun x -> either printType1Content printValidationFailure x)
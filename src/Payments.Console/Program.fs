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
// invoices |> Seq.iter (fun x -> printfn "Record %i, reference %s" x.InvoiceId x.InvoiceReference) |> ignore

let validatedInput = invoices |> Seq.map validatePayableInvoice

let printType1Content validatedRecord = 
    printfn "%s" (writeType1LineContent validatedRecord)

let printValidationFailure failure =
    printfn "Invoice Id: %d, Error: %A" failure.Result.InvoiceId failure.Errors

validatedInput |> Seq.iter (fun x -> either printType1Content printValidationFailure x)

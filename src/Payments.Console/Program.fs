open FSharp.Data.Sql
open System

let [<Literal>] connectionString = "Data Source=localhost;Initial Catalog=Payments;User Id=Payments;Password=Go0d2Use#"

type sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, connectionString, UseOptionTypes = Common.NullableColumnType.OPTION>

type PayableInvoice = {
    InvoiceId : int
    InvoiceReference : string
    Amount : decimal
    DueDate : DateTime
    ContactId : int
    AccountName : Option<string>
    AccountBsb : Option<string>
    AccountNumber : Option<string>
}

let createPayableInvoice (invoiceId, invoiceReference, amount, dueDate, contactId, accountName, accountBsb, accountNumber) = 
    {
        InvoiceId = invoiceId
        InvoiceReference = invoiceReference
        Amount = amount
        DueDate = dueDate
        ContactId = contactId
        AccountName = accountName
        AccountBsb = accountBsb
        AccountNumber = accountNumber
    }



let ctx = sql.GetDataContext()

let invoices = 
    query {
        for i in ctx.Dbo.Invoices do
        join c in ctx.Dbo.Contacts on (i.ContactId = c.ContactId)
        where (i.PaymentStatusId = 2)
        select (i.InvoiceId, i.InvoiceReference, i.Amount, i.DueDate, c.ContactId, c.AccountName, c.AccountBsb, c.AccountNumber)
    } 
    |> Seq.map createPayableInvoice

invoices |> Seq.iter (fun x -> printf "Record %i, reference %s" x.InvoiceId x.InvoiceReference) |> ignore






type ValidatedResult<'TValidated, 'TMessage> = 
    | Success of 'TValidated
    | Failure of 'TValidated * 'TMessage list

let validatedResult = Success "This was success"

let failedValidation = Failure (1, [ "message1"])

let printValidation result = match result with
                                | Success x -> printfn "%A " x
                                | Failure (x,y) -> printfn "%A %A" x y

printValidation validatedResult

printValidation failedValidation




module PayableInvoice

open System
open RoP

type PayableInvoice = {
    InvoiceId : int
    InvoiceReference : string
    Amount : decimal
    DueDate : DateTime
    ContactId : int
    AccountName : string option
    AccountBsb : string option
    AccountNumber : string option
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

type ValidationErrors = 
    | MissingAccountName
    | MissingBsb
    | MissingAccountNumber
    | InvalidAmount of amount : decimal

type FailureResult<'TResult> = {
    Result : 'TResult
    Errors : ValidationErrors list
}

let (&&&) v1 v2 =
    let addSuccess r1 r2 = r1 // return first
    let addFailure f1 f2 = { Result = f1.Result; Errors = (List.append f1.Errors f2.Errors) } // Combine errors
    plus addSuccess addFailure v1 v2

let validateAccountName payableInvoice =
    match payableInvoice.AccountName with
    | Some x -> Ok payableInvoice
    | None _ -> Error { Result = payableInvoice; Errors = [MissingAccountName]}

let validateAccountBsb payableInvoice =
    match payableInvoice.AccountBsb with
    | Some x -> Ok payableInvoice
    | None _ -> Error { Result = payableInvoice; Errors = [MissingBsb]}

let validateAccountNumber payableInvoice =
    match payableInvoice.AccountNumber with
    | Some x -> Ok payableInvoice
    | None _ -> Error { Result = payableInvoice; Errors = [MissingAccountNumber]}

let validateAmount payableInvoice = 
    if payableInvoice.Amount <= 0M then 
        Error { Result = payableInvoice; Errors = [InvalidAmount payableInvoice.Amount]}
    else
        Ok payableInvoice

let validatePayableInvoice =
    validateAccountName
    &&& validateAccountBsb
    &&& validateAccountNumber
    &&& validateAmount

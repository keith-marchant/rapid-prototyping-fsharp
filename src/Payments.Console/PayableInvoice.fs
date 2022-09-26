module PayableInvoice

open System

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
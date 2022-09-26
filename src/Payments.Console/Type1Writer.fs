module Type1Writer

open PayableInvoice
open System

let writeRecordType recordType = $"%1s{recordType}"
let writeTraceBsbNumberSummary traceBsbNumberSummary = $"%7s{traceBsbNumberSummary}"
let writeTraceAccountNumberSummary traceAccountNumberSummary = $"%9s{traceAccountNumberSummary}"
let writeIndicator indicator = $"%1s{indicator}"
let writeTransactionCodeSummary transactionCodeSummary = $"%2s{transactionCodeSummary}"
let private formatTotalAmount (totalAmount : decimal) = totalAmount.ToString("00000000.00").Replace(".", String.Empty)
let writeTotalAmount (totalAmount : decimal) = $"%10s{formatTotalAmount totalAmount}"
let writeTitleOfAccountSummaryLine titleOfAccountSummaryLine = $"%-32s{titleOfAccountSummaryLine}"
let writeLodgementReferenceSummary lodgementReferenceSummary = $"%-18s{lodgementReferenceSummary}"
let writeNameOfRemitterSummary nameOfRemitterSummary = $"%-16s{nameOfRemitterSummary}"
let writeAmountOfWithholdingTax amountOfWithholdingTax = $"%8d{amountOfWithholdingTax}"

let writeType1LineContent payableInvoice =
    writeRecordType "1" +
    writeTraceBsbNumberSummary "123-456" + 
    writeTraceAccountNumberSummary "123456789" +
    writeIndicator " " + 
    writeTransactionCodeSummary "13" + 
    writeTotalAmount payableInvoice.Amount +
    writeTitleOfAccountSummaryLine "COMPANY NAME" + 
    writeLodgementReferenceSummary "INVOICE PAYMENT" +
    writeTraceBsbNumberSummary "123-456" + 
    writeTraceAccountNumberSummary "123456789" +
    writeNameOfRemitterSummary "NAME" + 
    writeAmountOfWithholdingTax 0
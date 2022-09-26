namespace Payments.Functions

open System
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open Microsoft.Extensions.Logging

module CreateEftFile =
    [<FunctionName("CreateEftFile")>]
    let run([<TimerTrigger("0 */5 * * * *")>]myTimer: TimerInfo, log: ILogger) =
        let msg = sprintf "F# Time trigger function executed at: %A" DateTime.Now
        log.LogInformation msg

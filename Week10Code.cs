using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;

namespace Week10;
internal class Program
{
    static async Task Main(string[] args)
    {
        // this serviceConnectionString is stored in the code diectly in this example for demo purpose
        // it should be stored in the server when working for a business application.
        // ref: https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource?tabs=windows&pivots=platform-azp#store-your-connection-string
        string serviceConnectionString =  "YOUR_CONNECTION_STRING";
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = "Hello CIDM4360";
        // mailfrom domain of your email service on Azure
        var sender = "YourDomain";

        var htmlContent = @"
                    <html>
                        <body>
                            <h1 style=color:red>Testing Email for Azure Email Service</h1>
                            <h4>This is a HTML content</h4>
                            <p>Happy Learning!!</p>
                        </body>
                    </html>";

        Console.WriteLine("Please input recipient email address: ");
        string? recipient = Console.ReadLine();

        try
            {
               Console.WriteLine("Sending email...");
               EmailSendOperation emailSendOperation = await emailClient.SendAsync(Azure.WaitUntil.Completed, sender, recipient, subject, htmlContent);
               EmailSendResult statusMonitor = emailSendOperation.Value;

               string operationId = emailSendOperation.Id;
               var emailSendStatus = statusMonitor.Status;

               if (emailSendStatus == EmailSendStatus.Succeeded)
               {
                     Console.WriteLine($"Email send operation succeeded with OperationId = {operationId}.\nEmail is out for delivery.");
               }
               else
               {
                     var error = statusMonitor.Error;
                     Console.WriteLine($"Failed to send email.\n OperationId = {operationId}.\n Status = {emailSendStatus}.");
                     Console.WriteLine($"Error Code = {error.Code}, Message = {error.Message}");
                     return;
               }
            }
        catch (Exception ex)
            {
               Console.WriteLine($"Error in sending email, {ex}");
            }
    }
}


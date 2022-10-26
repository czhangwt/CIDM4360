using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
namespace Week10;
internal class Program
{
    static async Task Main(string[] args)
    {
        // this serviceConnectionString is stored in the code diectly in this example for demo purpose
        // it should be stored in the server when working for a business application.
        // ref: https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource?tabs=windows&pivots=platform-azp#store-your-connection-string
        string serviceConnectionString =  "YourConnectionString";
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = "Hello CIDM4360/5360 Week10";
        var emailContent = new EmailContent(subject);
        // use Multiline String @ to design html content
        emailContent.Html= @"
                    <html>
                        <body>
                            <h1 style=color:red>Testing Email for Azure Email Service</h1>
                            <h4>This is a HTML content</h4>
                            <p>Happy Learning!!</p>
                        </body>
                    </html>";


        // mailfrom domain of your email service on Azure
        var sender = "YourDomain";

        Console.WriteLine("Please input an email address: ");
        string inputEmail = Console.ReadLine();
        var emailRecipients = new EmailRecipients(new List<EmailAddress> {
            new EmailAddress(inputEmail) { DisplayName = "Testing" },
        });

        var emailMessage = new EmailMessage(sender, emailContent, emailRecipients);

        try
        {
            SendEmailResult sendEmailResult = emailClient.Send(emailMessage);

            string messageId = sendEmailResult.MessageId;
            if (!string.IsNullOrEmpty(messageId))
            {
                Console.WriteLine($"Email sent, MessageId = {messageId}");
            }
            else
            {
                Console.WriteLine($"Failed to send email.");
                return;
            }

            // wait max 2 minutes to check the send status for mail.
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            do
            {
                SendStatusResult sendStatus = emailClient.GetSendStatus(messageId);
                Console.WriteLine($"Send mail status for MessageId : <{messageId}>, Status: [{sendStatus.Status}]");

                if (sendStatus.Status != SendStatus.Queued)
                {
                    break;
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
               
            } while (!cancellationToken.IsCancellationRequested);

            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Looks like we timed out for email");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in sending email, {ex}");
        }
    }
}

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Threading.Tasks;

namespace AzureServiceBusTest
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([ServiceBusTrigger("punchqueue", Connection = "ServiceBusConnection")]Message message,
                               DateTime enqueuedTimeUtc,
                               int deliveryCount,
                               string contentType,
                               MessageReceiver messageReceiver,
                               string lockToken,
                               [ServiceBus("PunchQueue", Connection = "ServiceBusConnection")] IAsyncCollector<string> requeuePunch,
                               ILogger log)
        {
            var requeued = false;
            try
            {
                var body = System.Text.Encoding.UTF8.GetString(message.Body);
                await requeuePunch.AddAsync(body);
                requeued = true;
            }
            catch(Exception ex)
            {
                log.LogError($"Error while trying to requeue {ex.Message}");
            }
            if (requeued)
            {
                log.LogInformation("Requeued successfully");
                await messageReceiver.CompleteAsync(lockToken);
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                log.LogInformation("");
                await messageReceiver.AbandonAsync(lockToken);
            }
        }
    }
}

# AzureServiceBusRequeueTest
Test for message requeuing between BrokeredMessage and ServiceBus Message

The AddMessageToQueue project adds data to the queue, using Microsoft.ServiceBus.Messaging.BrokeredMessage class (from WindowsAzure.ServiceBus nuget v4.1.3)


The AzureServiceBusTest project contains an Azure function that listens on the service bus queue and requeues the message once it receives.
The Azure function uses Microsoft.Azure.ServiceBus.Message (from Microsoft.Azure.WebJobs.Extensions.ServiceBus nuget v3.0.3)

using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Reflection;

Console.WriteLine("*** Storage Queue Receiver ***");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly);

var config = builder.Build();
var queueConfig = config.GetSection("Queue");

var conString = queueConfig.GetValue<string>("ConString");
var queueName = "orders";

var client = new QueueClient(conString, queueName);

while (true)
{
    var msg = client.ReceiveMessage();

    if (msg.Value == null)
    {
        Console.WriteLine("nix msg");
    }
    else
    {
        Console.WriteLine(msg.Value.MessageId);
        Console.WriteLine(msg.Value.InsertedOn);
        Console.WriteLine(msg.Value.Body);
        client.DeleteMessage(msg.Value.MessageId, msg.Value.PopReceipt);
    }
    Thread.Sleep(1000);
}
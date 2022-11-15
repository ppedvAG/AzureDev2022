using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Reflection;

Console.WriteLine("*** Storage Queue Sender ***");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly);

var config = builder.Build();
var queueConfig = config.GetSection("Queue");

var conString = queueConfig.GetValue<string>("ConString");
var queueName = "orders";

var client = new QueueClient(conString, queueName);


for (int i = 0; i < 100; i++)
{
    await client.SendMessageAsync($"Test_{i:000}_{DateTime.Now:O}");
    Console.WriteLine($"Sent: {i:000}");
}
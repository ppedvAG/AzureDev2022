using Azure.Storage.Queues;
using Bogus;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

Console.WriteLine("*** Storage Queue Sender ***");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly);

var config = builder.Build();
var queueConfig = config.GetSection("Queue");

var conString = queueConfig.GetValue<string>("ConString");
var queueName = "orders";

var client = new QueueClient(conString, queueName);

var faker = new Faker<Product>().UseSeed(5);
faker.RuleFor(x => x.Name, x => x.Commerce.ProductName());
faker.RuleFor(x => x.Material, x => x.Commerce.ProductMaterial());
faker.RuleFor(x => x.Category, x => x.Commerce.Categories(1).First());

var prods = faker.Generate(100);

foreach (var p in prods)
{
    var json = JsonSerializer.Serialize(p);
    await client.SendMessageAsync(json);
    Console.WriteLine($"Sent: {p.Name}");
}

class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Material { get; set; }
}
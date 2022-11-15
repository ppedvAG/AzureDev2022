using Azure.Storage.Queues;

Console.WriteLine("*** Storage Queue Receiver ***");

var conString = "BlobEndpoint=https://start1234.blob.core.windows.net/;QueueEndpoint=https://start1234.queue.core.windows.net/;FileEndpoint=https://start1234.file.core.windows.net/;TableEndpoint=https://start1234.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=q&srt=o&sp=rwdlacup&se=2022-11-17T18:12:55Z&st=2022-11-15T10:12:55Z&spr=https&sig=MewkGysn0xAYZcOgeyjFqzzCc80x9nFXs%2FFdVMXI%2BtI%3D";
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
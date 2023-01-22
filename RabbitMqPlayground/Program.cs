// rabbit manual: 
// https://www.rabbitmq.com/dotnet-api-guide.html

using RabbitMqPlayground;

Console.WriteLine("Hello, World!");

var rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");
if (string.IsNullOrWhiteSpace(rabbitConnection))
{
    Console.WriteLine("No connection string. Sorry jako");
    return;
}

//create exchange and queues
var rabbitConnect = new RabbitConnect(rabbitConnection);

//create sender
var sender = new Sender(rabbitConnect);
await Task.Delay(100);

//send some messages
for (int i = 0; i < 10; i++)
{
    sender.SendMessage(i);
    await Task.Delay(10);
}

Console.WriteLine("press enter to start receiver");
Console.ReadLine();
//start receiver

//var r1 = new Receiver(rabbitConnect);
var r2 = new ReceiverManAck(rabbitConnect);

while (true)
{
    await Task.Delay(1000);
}



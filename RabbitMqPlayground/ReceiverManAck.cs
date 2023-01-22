using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqPlayground;

public class Receiver
{
    private RabbitConnect _rabbitConnect;
    private EventingBasicConsumer _consumer;

    public Receiver(RabbitConnect rabbitConnect)
    {
        _rabbitConnect = rabbitConnect;
        
        _consumer = new EventingBasicConsumer(_rabbitConnect.Channel);
        _consumer.Received += ProcessMessage;
        
        _rabbitConnect.Channel.BasicConsume(queue: RabbitConnect.Q1Name,
            autoAck: true,
            consumer: _consumer);
    }

    private void ProcessMessage(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"received (autoAckReceiver) => {message}, press any key to continue ");
        Console.ReadLine();

    }


}
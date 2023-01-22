using System.Text;
using RabbitMQ.Client;

namespace RabbitMqPlayground;

public class Sender
{
    private RabbitConnect _rabbitConnect;

    public Sender(RabbitConnect rabbitConnect)
    {
        _rabbitConnect = rabbitConnect;
    }

    public void SendMessage(int messageNumber)
    {
        var message = $"message {messageNumber}";
        var body = Encoding.UTF8.GetBytes(message);
        IBasicProperties props = _rabbitConnect.Channel.CreateBasicProperties();
        props.Priority = 1;
        props.DeliveryMode = 2;

        _rabbitConnect.Channel.BasicPublish(exchange: RabbitConnect.ExchangeName,
            routingKey: "",
            basicProperties: props,
            body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }

    
}
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqPlayground;

public class Sender
{
    private IConnection _connection;
    private IModel _channel;

    private const string ExchangeName = "blablabla";

    public Sender(string connectionString)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable:true, autoDelete:false);
    }

    public void SendMessage(int messageNumber)
    {
        var message = $"message {messageNumber}";
        var body = Encoding.UTF8.GetBytes(message);
        // IBasicProperties props = _channel.CreateBasicProperties();
        // props.Priority = 3;
        _channel.BasicPublish(exchange: ExchangeName,
            routingKey: "",
            basicProperties: null,
            body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }

    
}
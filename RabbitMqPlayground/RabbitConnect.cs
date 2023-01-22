using System.Text;
using RabbitMQ.Client;

namespace RabbitMqPlayground;

public class RabbitChannel
{
    private IConnection _connection;
    private IModel _channel;

    private const string ExchangeName = "blablabla";

    public RabbitChannel(string connectionString)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable:true, autoDelete:false);
    }
    
}
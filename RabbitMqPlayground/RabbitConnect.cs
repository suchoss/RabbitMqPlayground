using System.Text;
using RabbitMQ.Client;

namespace RabbitMqPlayground;

public class RabbitConnect
{
    public IConnection Connection;
    public IModel Channel;

    public const string ExchangeName = "blablabla";
    public const string Q1Name = "blablaQueue1";
    public const string Q2Name = "blablaQueue2";

    public RabbitConnect(string connectionString)
    {
        var con = connectionString.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(k => k[0], v => v[1]);
        
        
        var factory = new ConnectionFactory()
        {
            HostName = con["host"],
            UserName = con["username"],
            Password = con["password"]
        };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
        Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable:true, autoDelete:false);
        Channel.QueueDeclare(Q1Name, durable:true, exclusive:false, autoDelete:false);
        Channel.QueueDeclare(Q2Name, durable:true, exclusive:false, autoDelete:false);
        Channel.QueueBind(queue: Q1Name,
            exchange: ExchangeName,
            routingKey: "");
        Channel.QueueBind(queue: Q2Name,
            exchange: ExchangeName,
            routingKey: "");
    }
    
}
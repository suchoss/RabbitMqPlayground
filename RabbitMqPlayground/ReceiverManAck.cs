using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqPlayground;

public class ReceiverManAck
{
    private RabbitConnect _rabbitConnect;
    private EventingBasicConsumer _consumer;

    public ReceiverManAck(RabbitConnect rabbitConnect)
    {
        _rabbitConnect = rabbitConnect;
        
        _consumer = new EventingBasicConsumer(_rabbitConnect.Channel);
        _consumer.Received += ProcessMessage;
        
        // čte z druhé fronty, aby se to nemotalo
        _rabbitConnect.Channel.BasicConsume(queue: RabbitConnect.Q2Name,
            autoAck: false,
            consumer: _consumer);
    }

    private void ProcessMessage(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"received (autoAckReceiver) => {message}, \n  (y) to acknowledge, other key will not acknowledge processing ");
        var answer = Console.ReadLine();
        if (answer == "y")
        {
            //acknowledge processing => remove from queue
            _rabbitConnect.Channel.BasicAck(e.DeliveryTag, false);
        }
        else
        {
            //not confirming processing => returns back to queue
        }
        
        
        

    }


}
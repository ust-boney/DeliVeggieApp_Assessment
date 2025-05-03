using RabbitMQ.Client;
using System.Text;

namespace VeggieAppConsole.MessageBroker
{
    public class Publisher
    {
        private readonly string _hostname = "rabbitmq";
        private readonly int _port = 5672;
        private readonly string _queueName = "product-queue";

        public void SendMessage(string message)
        {
            using var connection = Common.TryConnectWithRetry();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: properties,
                                 body: body);

            Console.WriteLine($"[x] Sent: {message}");
        }
    }
}

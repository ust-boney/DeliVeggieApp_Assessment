using RabbitMQ.Client;
using System.Text;

namespace DeliVeggieApp.WebApi.MessageBroker
{
    public class Publisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "product-queue";
       

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname
            };
            using var connection = factory.CreateConnection();
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

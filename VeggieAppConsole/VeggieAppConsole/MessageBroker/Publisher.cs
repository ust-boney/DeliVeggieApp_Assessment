using RabbitMQ.Client;
using System.Text;

namespace VeggieAppConsole.MessageBroker
{
    public class Publisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "product-queue";
        private readonly string _queueName2 = "productdetail-queue";
        //private readonly string _username = "guest";
        //private readonly string _password = "guest";

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName2,
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

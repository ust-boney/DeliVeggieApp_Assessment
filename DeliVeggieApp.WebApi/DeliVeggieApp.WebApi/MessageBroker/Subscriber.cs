using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace DeliVeggieApp.WebApi.MessageBroker
{
    public class Subscriber : BackgroundService
    {
        private readonly string _hostname = "rabbitmq";
        private readonly int _port = 5672;
        private readonly string _queueName = "product-queue";
      
        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received: {message}");

                
            };

            channel.BasicConsume(queue: _queueName,
                           autoAck: true, // Set to false if you want manual ack
                           consumer: consumer);

            Console.WriteLine(" [*] Waiting for messages. Press [enter] to exit.");
            Console.ReadLine();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           ReceiveMessage();
           return Task.CompletedTask;
        }
    }
}

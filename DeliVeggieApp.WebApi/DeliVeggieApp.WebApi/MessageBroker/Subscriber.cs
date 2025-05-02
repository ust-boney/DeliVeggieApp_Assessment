using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace DeliVeggieApp.WebApi.MessageBroker
{
    public class Subscriber : BackgroundService
    {
        private readonly string _hostname = "localhost";
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

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received: {message}");

                // Simulate processing (optional)
                // Task.Delay(1000).Wait();

                // Acknowledge message manually if needed
                // channel.BasicAck(ea.DeliveryTag, multiple: false);
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

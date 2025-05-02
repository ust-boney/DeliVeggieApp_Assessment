using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Hosting;
using static System.Formats.Asn1.AsnWriter;
using System.Text.Json;
using VeggieAppConsole.Services;
using VeggieAppConsole.Models;

namespace VeggieAppConsole.MessageBroker
{
    public class Subscriber :BackgroundService
    {
        private readonly string _hostname = "rabbitmq";
        private readonly int _port = 5672;
        private readonly string _queueName = "product-queue";
        private IProductService _productService;
        public Subscriber(IProductService productService)
        {
            _productService = productService;
        }
        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = _hostname, Port=_port, UserName = "guest", Password = "guest" };
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
                PublishMessage(message);

            };

            channel.BasicConsume(queue: _queueName,
                           autoAck: true, // Set to false if you want manual ack
                           consumer: consumer);

            Console.WriteLine(" [*] Waiting for messages. Press [enter] to exit.");
           
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ReceiveMessage();
            return Task.CompletedTask;
        }

        private void PublishMessage(string serviceName)
        {
            string jsonResult =string.Empty;
            if (serviceName == "GetProductList")
            {
               var products= _productService.GetAll();
                jsonResult = JsonSerializer.Serialize(products);
            }
            else if (serviceName.Contains("GetProductDetails"))
            {
                int parameterId = 0;
                int.TryParse(serviceName.Split('/')[1], out  parameterId);
                var product = _productService.GetById(parameterId);
                 jsonResult = JsonSerializer.Serialize(product);
            }
            var publisher = new Publisher();
            publisher.SendMessage(jsonResult);

            Console.WriteLine("Published message");
            Console.ReadLine();
        }
    }
}

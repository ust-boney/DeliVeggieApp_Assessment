using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
namespace VeggieAppConsole.MessageBroker
{
    public class Common
    {
        public static IConnection TryConnectWithRetry(
    int maxRetries = 10, int delayMilliseconds = 2000)
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS")
            };

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    return factory.CreateConnection();
                }
                catch (BrokerUnreachableException)
                {
                    Console.WriteLine($"[Retry {i + 1}] RabbitMQ not ready. Waiting...");
                    Thread.Sleep(delayMilliseconds);
                }
            }

            throw new Exception("Failed to connect to RabbitMQ after multiple retries.");
        }
    }
}
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = new { Name = "DK", Message = "Hello i am new to queue" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "demo-queue", null, body);
            var consumerCount= channel.ConsumerCount("demo-queue");

            Console.WriteLine("Hello World!"+ consumerCount);
        }
    }


}

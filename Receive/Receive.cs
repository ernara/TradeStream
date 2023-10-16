using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TradeStream;

var trades = new List<Trade>();

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "trades",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    var trade = JsonSerializer.Deserialize<Trade>(message);

    if (trade != null)
    {
        trades.Add(trade);
    }

    //Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(queue: "trades",
                     autoAck: true,
                     consumer: consumer);

while (true)
{
    Console.WriteLine($"Total trades: {trades.Count}");
    var title = Console.ReadLine();

    var filtered = trades.Where(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase))
        .ToList();

    foreach (var trade in filtered)
    {
        Console.WriteLine($"trade: {trade}");
    }
}
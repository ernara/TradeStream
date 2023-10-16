using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using RabbitMQ.Client;
using TradeStream;

var generator = new TradeGenerator();


var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "trades",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

//channel.QueueBind("Trades", string.Empty, "Trades");

while (true)
{
    var trade = generator.NextTrade();

    var json = JsonSerializer.Serialize(trade);

    var body = Encoding.UTF8.GetBytes(json);


    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "trades",
                         basicProperties: null,
                         body: body);


    Console.WriteLine($" [x] Sent {json}");

    Thread.Sleep(Random.Shared.Next(500, 500));
}

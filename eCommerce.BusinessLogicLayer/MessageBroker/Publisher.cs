using eCommerce.BusinessLogicLayer.MessageBroker.Abstractions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace eCommerce.BusinessLogicLayer.MessageBroker;

public class Publisher : IPublisher
{
    private readonly IConnector _connector;
    private readonly string _exchange;
    public Publisher(IConnector connector, IConfiguration cfg)
    {
        _connector = connector;
        _exchange = cfg["RABBITMQ_PRODUCTS_EXCHANGE"]!;
    }
    public void Publish<T>(string routeKey, T message)
    {
        using var channel = _connector.CreateChannel();

        // creating the exchange
        // it will open it if it already exists;
        channel.ExchangeDeclare(
            exchange: _exchange,
            type: ExchangeType.Direct,
            autoDelete: false);


        // sending the message through exchange
        var body = Encoding.UTF8
            .GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: _exchange,
            routingKey: routeKey,
            basicProperties: null,
            body: body);
    }
}
